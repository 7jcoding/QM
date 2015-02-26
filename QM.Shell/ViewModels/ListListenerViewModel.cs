using QM.Shell.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.ViewModels {
    public class ListListenerViewModel : SchedulerScreen {
        public override string Title {
            get {
                return "触发器监听器列表";
            }
        }

        private IScheduler Scheduler;

        /// <summary>
        /// 触发器监听器列表
        /// </summary>
        public List<ITriggerListener> TriggerListeners {
            get;
            set;
        }

        /// <summary>
        /// 高度服务监听器列表
        /// </summary>
        public List<ISchedulerListener> SchedulerListeners {
            get;
            set;
        }

        /// <summary>
        /// 任务监听器列表
        /// </summary>
        public List<IJobListener> JobListeners {
            get;
            set;
        }

        public override void Update(Quartz.IScheduler scheduler) {
            this.Scheduler = scheduler;
            //NOT SUPPORT
            this.TriggerListeners = scheduler.ListenerManager.GetTriggerListeners().ToList();
            this.SchedulerListeners = scheduler.ListenerManager.GetSchedulerListeners().ToList();
            this.JobListeners = scheduler.ListenerManager.GetJobListeners().ToList();
            this.NotifyOfPropertyChange(() => this.TriggerListeners);
            this.NotifyOfPropertyChange(() => this.SchedulerListeners);
            this.NotifyOfPropertyChange(() => this.JobListeners);
        }

        public override void Refresh() {
            this.Update(this.Scheduler);
        }
    }
}
