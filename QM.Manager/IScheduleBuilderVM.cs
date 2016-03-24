using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Manager {
    public interface IScheduleBuilderVM {
        ScheduleBuilderTypes Type {
            get;
        }

        BaseScheduleBuilderInfo Info { get; }

        void Restore(BaseScheduleBuilderInfo info);
    }
}
