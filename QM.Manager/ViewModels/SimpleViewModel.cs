using QM.Manager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QM.Server.WebApi.Entity;

namespace QM.Manager.ViewModels {

    [Regist(InstanceMode.PreRequest, ForType = typeof(IScheduleBuilderVM))]
    public class SimpleViewModel : BaseScreen, IScheduleBuilderVM {
        public override string Title {
            get {
                return "Simple";
            }
        }

        public ScheduleBuilderTypes Type {
            get {
                return ScheduleBuilderTypes.Simple;
            }
        }

        public SimpleScheduleBuilderInfo Data { get; set; }

        public BaseScheduleBuilderInfo Info {
            get {
                return this.Data;
            }
        }

        public SimpleViewModel() {
            this.Data = new SimpleScheduleBuilderInfo();
        }

        public void Restore(BaseScheduleBuilderInfo info) {
            this.Data = (SimpleScheduleBuilderInfo)info;
            this.NotifyOfPropertyChange(() => this.Data);
        }

        public override Task Update() {
            return Task.FromResult<object>(null);
        }
    }
}
