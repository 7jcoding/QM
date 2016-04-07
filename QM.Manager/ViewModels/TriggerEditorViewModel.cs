using Caliburn.Micro;
using QM.Manager.Common;
using QM.Server.ApiClient;
using QM.Server.ApiClient.Methods;
using QM.Server.WebApi.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QM.Manager.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class TriggerEditorViewModel : BaseScreen {
        public override string Title {
            get {
                return "触发器编辑";
            }
        }

        private string Name, Group;

        public TriggerInfo Data { get; set; }

        private IScheduleBuilderVM _scheduleBuilderVM = null;
        public IScheduleBuilderVM ScheduleBuilderVM {
            get {
                return this._scheduleBuilderVM;
            }
            set {
                this._scheduleBuilderVM = value;
                this.NotifyOfPropertyChange(() => this.ScheduleBuilderVM);
            }
        }

        public IEnumerable<IScheduleBuilderVM> AvaliableScheduleBuilderVM {
            get; set;
        }

        public TriggerEditorViewModel(SimpleContainer container) {
            this.AvaliableScheduleBuilderVM = container.GetAllInstances<IScheduleBuilderVM>();
            this.ScheduleBuilderVM = this.AvaliableScheduleBuilderVM.FirstOrDefault();
            this.NotifyOfPropertyChange(() => this.ScheduleBuilderVM);
            this.NotifyOfPropertyChange(() => this.AvaliableScheduleBuilderVM);
        }

        public async override Task Update() {
            if (!string.IsNullOrWhiteSpace(this.Name)) {
                var mth = new GetTrigger() {
                    Name = this.Name,
                    Group = this.Group
                };
                this.Data = await ApiClient.Instance.Execute(mth);
            } else {
                this.Data = null;
            }

            this.NotifyOfPropertyChange(() => this.Data);
        }


        public async Task Update(string name, string group) {
            this.Name = name;
            this.Group = group;
            await this.Update();
        }

        public void Update(JobInfo job) {
            this.Data = new TriggerInfo() {
                JobName = job.Name,
                JobGroup = job.Group,
                JobType = job.JobType, //job.JobTypeFullName,
                JobDesc = job.Desc,
                JobDataMap = job.DataMap
            };
            this.NotifyOfPropertyChange(() => this.Data);
        }

        public async void Save() {
            var mth = new SaveTrigger() {
                Calendar = null,
                JobGroup = this.Data.JobGroup,
                JobName = this.Data.JobName,
                Priority = this.Data.Priority,
                TriggerDesc = this.Data.Desc,
                TriggerGroup = this.Data.TriggerGroup,
                TriggerName = this.Data.TriggerName,
                ScheduleBuilderInfo = this.ScheduleBuilderVM.Info
            };

            var result = await ApiClient.Instance.Execute(mth);
            MessageBox.Show(result.ToString());
        }
    }
}
