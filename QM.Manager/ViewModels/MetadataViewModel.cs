using QM.Manager.Common;
using QM.Server.ApiClient;
using QM.Server.ApiClient.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Manager.ViewModels {
    public class MetadataViewModel : BaseScreen {
        public override string Title {
            get {
                return "服务概览";
            }
        }

        public Dictionary<string, string> Datas { get; set; }

        public async override void Update() {
            var metadata = await ApiClient.Instance.Execute(new GetMetadata());
            this.Datas = metadata;
            this.NotifyOfPropertyChange(() => this.Datas);
        }
    }
}
