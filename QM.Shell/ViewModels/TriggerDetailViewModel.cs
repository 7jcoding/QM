using Caliburn.Micro;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QM.Shell.ViewModels {
    /// <summary>
    /// Trigger 详细信息界面
    /// </summary>
    public class TriggerDetailViewModel : PropertyChangedBase {

        /// <summary>
        /// 是否可暂停
        /// </summary>
        public bool CanPause {
            get;
            set;
        }

        /// <summary>
        /// 是否可恢复
        /// </summary>
        public bool CanResume {
            get;
            set;
        }

        /// <summary>
        /// 是否可删除
        /// </summary>
        public bool CanDelete {
            get;
            set;
        }


        ///// <summary>
        ///// 是否可中断
        ///// </summary>
        //public bool Interrupt {
        //    get;
        //    set;
        //}


        /// <summary>
        /// 上一次执行时间
        /// </summary>
        public DateTimeOffset? PrevTime {
            get;
            set;
        }

        /// <summary>
        /// 下一次执行时间
        /// </summary>
        public DateTimeOffset? NextTime {
            get;
            set;
        }

        /// <summary>
        /// 任务类型 (IJob)
        /// </summary>
        public string JobType {
            get;
            set;
        }

        /// <summary>
        /// 触发器状态
        /// </summary>
        public TriggerState State {
            get;
            set;
        }

        /// <summary>
        /// 参数界面
        /// </summary>
        public JobDataMapDetailViewModel JobDataMapVM {
            get;
            set;
        }

        private IScheduler Scheduler;
        private ITrigger Trigger;


        public System.Action Refresh {
            get;
            set;
        }

        public TriggerDetailViewModel() {
            this.JobDataMapVM = new JobDataMapDetailViewModel();
        }
        public void Update(IScheduler scheduler, ITrigger trigger) {
            this.Scheduler = scheduler;
            this.Trigger = trigger;
            var job = this.Scheduler.GetJobDetail(this.Trigger.JobKey);

            this.PrevTime = this.Trigger.GetPreviousFireTimeUtc();
            this.NextTime = this.Trigger.GetNextFireTimeUtc();
            this.JobType = job.JobType.AssemblyQualifiedName;

            this.CanDelete = true;

            this.NotifyOfPropertyChange(() => this.PrevTime);
            this.NotifyOfPropertyChange(() => this.NextTime);
            this.NotifyOfPropertyChange(() => this.JobType);
            this.NotifyOfPropertyChange(() => this.CanDelete);

            this.ChangeState();
            this.JobDataMapVM.Update(job.JobDataMap);
        }

        /// <summary>
        /// 设置按钮的是否用
        /// </summary>
        private void ChangeState() {
            this.CanResume = false;
            this.CanPause = false;

            var state = this.Scheduler.GetTriggerState(this.Trigger.Key);
            switch (state) {
                case TriggerState.Normal:
                    this.CanPause = true;
                    break;
                case TriggerState.Paused:
                case TriggerState.Complete:
                case TriggerState.Error:
                    this.CanResume = true;
                    break;
            }
            this.State = state;
            this.NotifyOfPropertyChange(() => this.State);
            this.NotifyOfPropertyChange(() => this.CanPause);
            this.NotifyOfPropertyChange(() => this.CanResume);
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause() {
            if (this.Scheduler != null && this.Trigger != null)
                this.Scheduler.PauseTrigger(this.Trigger.Key);

            this.ChangeState();
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public void Resume() {
            if (this.Scheduler != null && this.Trigger != null)
                this.Scheduler.ResumeTrigger(this.Trigger.Key);

            this.ChangeState();
        }

        public void Delete() {
            if (MessageBox.Show("确定要删除指定任务？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                var flag = false;
                if (this.Scheduler != null && this.Trigger != null)
                    flag = this.Scheduler.DeleteJob(this.Trigger.JobKey);

                if (this.Refresh != null)
                    Refresh.Invoke();

                if (!flag) {
                    MessageBox.Show("删除失败");
                }
            }
        }

        public void Interrupt() {
            if (MessageBox.Show("是否要立即中断(取消)该次任务？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                if (this.Scheduler != null && this.Trigger != null) {
                    var flag = true;
                    try {
                        flag = this.Scheduler.Interrupt(this.Trigger.JobKey);
                    } catch {
                    }

                    if (!flag) {
                        MessageBox.Show("中断(取消)失败, 要中断的任务必须是实现IInterruptableJob");
                    }

                    if (this.Refresh != null)
                        Refresh.Invoke();
                }
            }
        }

        public void Edit() {
            var vm = new EditJobViewModel();
            vm.SetJob(this.Scheduler, this.Trigger);
            GlobalData.GetShell().ShowAddJob(vm);
        }
    }
}
