using Caliburn.Micro;
using QM.Shell.Common;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.ViewModels {

    /// <summary>
    /// Jobs 列表
    /// </summary>
    public class ListJobsViewModel : SchedulerScreen {

        private IScheduler Scheduler;
        public BindableCollection<IJobDetail> Jobs {
            get;
            private set;
        }

        public override void Update(IScheduler scheduler) {
            this.Scheduler = scheduler;

            var grps = this.Scheduler.GetJobGroupNames();
            this.Jobs = new BindableCollection<IJobDetail>(grps.SelectMany(grp => this.Scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(grp)))
                .Select(key => this.Scheduler.GetJobDetail(key)));
            this.NotifyOfPropertyChange(() => this.Jobs);
        }

        public override string Title {
            get {
                return "任务列表";
            }
        }
    }
}
