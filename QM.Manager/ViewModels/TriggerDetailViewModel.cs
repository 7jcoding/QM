using QM.Manager.Common;
using QM.Server.ApiClient;
using QM.Server.ApiClient.Methods;
using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QM.Manager.ViewModels {
    public class TriggerDetailViewModel : BaseScreen {
        public override string Title {
            get {
                return "触发器";
            }
        }

        private TriggerInfo _data = null;
        public TriggerInfo Data {
            get {
                return this._data;
            }
            private set {
                this._data = value;
                this.JobDataMapVM.Update(value != null ? value.JobDataMap : null);
                this.TriggerDataMapVM.Update(value != null ? value.TriggerDataMap : null);
            }
        }

        public DataMapViewModel JobDataMapVM { get; set; }
        public DataMapViewModel TriggerDataMapVM { get; set; }

        public TriggerDetailViewModel() {
            this.JobDataMapVM = new DataMapViewModel();
            this.TriggerDataMapVM = new DataMapViewModel();
        }

        public async override Task Update() {
            if (this.Data == null)
                return;

            var method = new GetTrigger() {
                Name = this.Data.TriggerName,
                Group = this.Data.TriggerGroup
            };
            var trigger = await ApiClient.Instance.Execute(method);
            if (trigger == null) {
                MessageBox.Show("触发器不存在，或已执行完");
            } else {
                this.Data = trigger;
                this.NotifyOfPropertyChange(() => this.Data);
            }
        }

        public void Update(TriggerInfo trigger) {
            this.Data = trigger;
            this.NotifyOfPropertyChange(() => this.Data);
        }
    }
}
