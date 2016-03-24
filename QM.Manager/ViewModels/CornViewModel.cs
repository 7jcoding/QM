using QM.Manager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QM.Server.WebApi.Entity;

namespace QM.Manager.ViewModels {

    [Regist(InstanceMode.PreRequest, ForType = typeof(IScheduleBuilderVM))]
    public class CornViewModel : BaseScreen, IScheduleBuilderVM {
        public override string Title {
            get {
                return "Corn 编辑";
            }
        }

        public ScheduleBuilderTypes Type {
            get {
                return ScheduleBuilderTypes.Cron;
            }
        }

        public CronScheduleBuilderInfo Data { get; set; }

        public BaseScheduleBuilderInfo Info {
            get {
                return this.Data;
            }
        }

        public CornViewModel() {
            this.Data = new CronScheduleBuilderInfo();
        }

        public void Restore(BaseScheduleBuilderInfo info) {
            this.Data = (CronScheduleBuilderInfo)info;
        }

        public override Task Update() {
            return Task.FromResult<object>(null);
        }
    }
}
