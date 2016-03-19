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
    public class JobEditorViewModel : BaseScreen {
        public override string Title {
            get {
                return "任务编辑";
            }
        }

        private string Name = "";
        private string Group = "";

        public JobInfo Data { get; set; }

        public async override Task Update() {
            if (string.IsNullOrWhiteSpace(this.Name))
                this.Data = null;
            else {
                var mth = new GetJob() {
                    Group = this.Group,
                    Name = this.Name
                };
                this.Data = await ApiClient.Instance.Execute(mth);
            }
            this.NotifyOfPropertyChange(() => this.Data);
        }

        public async void Update(string name, string group) {
            this.Name = name;
            this.Group = group;
            await this.Update();
        }
    }
}
