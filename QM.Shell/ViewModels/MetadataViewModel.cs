using QM.Shell.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.ViewModels {

    /// <summary>
    /// 概览界面
    /// </summary>
    public class MetadataViewModel : SchedulerScreen {

        public string Summary {
            get;
            set;
        }

        public SchedulerMetaData Metadata {
            get;
            set;
        }

        private IScheduler Scheduler = null;
        public override void Update(IScheduler scheduler) {
            this.Scheduler = scheduler;

            this.Metadata = scheduler.GetMetaData();
            this.Summary = this.Metadata.GetSummary();

            this.NotifyOfPropertyChange(() => this.Metadata);
            this.NotifyOfPropertyChange(() => this.Summary);
        }

        public override void Refresh() {
            this.Update(this.Scheduler);
        }

        public override string Title {
            get {
                return "服务概览";
            }
        }
    }
}
