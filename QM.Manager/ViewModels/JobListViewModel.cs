using Microsoft.Win32;
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
    public class JobListViewModel : BaseScreen {
        public override string Title {
            get {
                return "任务列表";
            }
        }

        public IEnumerable<JobInfo> Datas { get; set; }

        private JobInfo _current = null;
        public JobInfo Current {
            get {
                return this._current;
            }
            set {
                this._current = value;
                this.DataMapVM.Update(value != null ? value.DataMap : null);
            }
        }

        public DataMapViewModel DataMapVM { get; set; }

        public JobListViewModel() {
            this.DataMapVM = new DataMapViewModel();
        }

        public async override Task Update() {
            var mth = new GetJobs();
            this.Datas = await ApiClient.Instance.Execute(mth);
            this.NotifyOfPropertyChange(() => this.Datas);
        }

        public void Upload() {
            var dlg = new OpenFileDialog() {
                Title = "打包上传任务",
                Filter = "zip|*.zip"
            };
            if (dlg.ShowDialog() == true) {
                var file = dlg.FileName;
            }
        }
    }
}
