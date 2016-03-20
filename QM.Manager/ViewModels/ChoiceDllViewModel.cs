using QM.Manager.Common;
using QM.Server.ApiClient;
using QM.Server.ApiClient.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Manager.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ChoiceDllViewModel : BaseScreen {
        public override string Title {
            get {
                return "选择DLL";
            }
        }

        public IEnumerable<string> Datas { get; set; }

        private string _current;
        public string Current {
            get {
                return this._current;
            }
            set {
                this._current = value;
                this.NotifyOfPropertyChange(() => this.CanChoice);
            }
        }

        public bool CanChoice {
            get {
                return !string.IsNullOrWhiteSpace(this.Current);
            }
        }

        public override async Task Update() {
            var mth = new GetDlls();
            this.Datas = await ApiClient.Instance.Execute(mth);
            this.NotifyOfPropertyChange(() => this.Datas);
        }

        public async void Choice() {
            var mth = new GetJobTypes() {
                DllPath = this.Current
            };
            var datas = await ApiClient.Instance.Execute(mth);
        }
    }
}
