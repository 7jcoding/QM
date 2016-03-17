using QM.Manager.Common;
using QM.Server.ApiClient;
using QM.Server.ApiClient.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Manager.ViewModels {
    public class TriggerListViewModel : BaseScreen {
        public override string Title {
            get {
                return "触发器列表";
            }
        }

        public async override void Update() {
            var mth = new GetTriggers();
            var datas = await ApiClient.Instance.Execute(mth);
        }
    }
}
