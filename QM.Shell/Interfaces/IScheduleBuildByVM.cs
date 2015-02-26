using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.Interfaces {
    public interface IScheduleBuildByVM {

        string Name {
            get;
        }

        Type ApplyTo {
            get;
        }

        IScheduleBuilder GetScheduleBuilder();

        void Restore(IScheduleBuilder builder);
    }
}
