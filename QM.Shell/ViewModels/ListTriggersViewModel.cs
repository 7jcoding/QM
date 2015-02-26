using Caliburn.Micro;
using QM.Shell.Common;
using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.ViewModels {
    public class ListTriggersViewModel : SchedulerScreen {

        private IScheduler Scheduler;

        public BindableCollection<ITrigger> Triggers {
            get;
            private set;
        }

        private ITrigger currentTrigger;
        public ITrigger CurrentTrigger {
            get {
                return this.currentTrigger;
            }
            set {
                this.currentTrigger = value;
                if (value == null) {
                    this.NotifyOfPropertyChange(() => this.TriggerDetailVM);

                } else if (this.Scheduler != null)
                    this.TriggerDetailVM.Update(this.Scheduler, value);
            }
        }

        private TriggerDetailViewModel triggerDetailVM = null;
        public TriggerDetailViewModel TriggerDetailVM {
            get {
                if (this.triggerDetailVM == null) {
                    this.triggerDetailVM = new TriggerDetailViewModel();
                    this.TriggerDetailVM.Refresh = Refresh;
                }
                return this.triggerDetailVM;
            }
        }

        public DateTimeOffset CatchTime {
            get;
            set;
        }

        public override void Update(IScheduler scheduler) {
            this.Scheduler = scheduler;
            this.CatchTime = DateTime.Now;

            var grps = this.Scheduler.GetTriggerGroupNames();
            this.Triggers = new BindableCollection<ITrigger>(grps.SelectMany(grp => this.Scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(grp)))
                .Select(key => {
                    try {
                        return this.Scheduler.GetTrigger(key);
                    } catch {
                        return null;
                    }
                }).Where(t => t != null));
            this.NotifyOfPropertyChange(() => this.Triggers);
            this.NotifyOfPropertyChange(() => this.TriggerDetailVM);
            this.NotifyOfPropertyChange(() => this.CatchTime);
        }

        public override void Refresh() {
            this.Update(this.Scheduler);
            this.CurrentTrigger = null;
            this.NotifyOfPropertyChange(() => this.CurrentTrigger);
        }

        public override string Title {
            get {
                return "触发器列表";
            }
        }
    }
}
