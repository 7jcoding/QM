using QM.Manager.Common;
using QM.Server.ApiClient;
using QM.Server.ApiClient.Methods;
using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
