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
using System.Windows;

namespace QM.Manager.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class UploadViewModel : BaseScreen {
        public override string Title {
            get {
                return "打包上传任务";
            }
        }

        public string File { get; set; }

        public string SavePath { get; set; }

        public override Task Update() {
            return Task.FromResult<object>(null);
        }

        public void ChoiceFile() {
            var dlg = new OpenFileDialog() {
                Title = "打包上传任务",
                Filter = "zip|*.zip"
            };

            if (dlg.ShowDialog() == true) {
                this.File = dlg.FileName;
                this.NotifyOfPropertyChange(() => this.File);
            }
        }

        public async void Upload() {
            var mth1 = new UploadPrevCheck(this.SavePath, this.File);
            var result = await ApiClient.Instance.Execute(mth1);
            if (result == UploadStates.Success) {
                var mth2 = new Upload() {
                    FilePath = this.File,
                    Name = this.SavePath
                };
                result = await ApiClient.Instance.Execute(mth2);
            }
            MessageBox.Show(result.ToString());
        }
    }
}
