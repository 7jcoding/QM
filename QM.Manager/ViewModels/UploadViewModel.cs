using Microsoft.Win32;
using QM.Manager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Upload() {

        }
    }
}
