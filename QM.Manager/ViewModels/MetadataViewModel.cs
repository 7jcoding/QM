using QM.Manager.Common;
using QM.Server.ApiClient;
using QM.Server.ApiClient.Methods;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QM.Manager.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MetadataViewModel : BaseScreen {
        public override string Title {
            get {
                return "服务概览";
            }
        }

        public Dictionary<string, string> Datas { get; set; }

        public async override Task Update() {
            var metadata = await ApiClient.Instance.Execute(new GetMetadata());
            this.Datas = metadata;
            this.NotifyOfPropertyChange(() => this.Datas);
        }
    }
}
