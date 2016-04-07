using Caliburn.Micro;
using QM.Manager.Common;
using QM.Server.ApiClient;
using QM.Server.ApiClient.Methods;
using QM.Server.WebApi.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QM.Manager.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class TriggerListViewModel : BaseScreen {
        public override string Title {
            get {
                return "触发器列表";
            }
        }

        public IEnumerable<TriggerInfo> Datas { get; set; }

        public TriggerDetailViewModel TriggerDetailVM { get; set; }

        private TriggerInfo _current = null;
        public TriggerInfo Current {
            get {
                return this._current;
            }
            set {
                this._current = value;
                this.TriggerDetailVM.Update(value);
            }
        }

        public TriggerListViewModel(SimpleContainer container) {
            this.TriggerDetailVM = container.GetInstance<TriggerDetailViewModel>();
        }

        public async override Task Update() {
            var mth = new GetTriggers();
            var datas = await ApiClient.Instance.Execute(mth);
            this.Datas = datas;
            this.NotifyOfPropertyChange(() => this.Datas);
        }
    }
}
