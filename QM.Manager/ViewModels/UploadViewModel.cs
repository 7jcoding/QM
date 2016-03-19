using QM.Manager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Manager.ViewModels {
    public class UploadViewModel : BaseScreen {
        public override string Title {
            get {
                return "打包上传任务";
            }
        }

        public override Task Update() {
            return Task.FromResult<object>(null);
        }
    }
}
