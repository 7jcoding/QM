using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobA {

    public abstract class BaseJob : IJob {

        public abstract void Execute(IJobExecutionContext context);
    }

    [Description("基类在同一个DLL的中的示例")]
    public class BaseClassJob : BaseJob {
        public override void Execute(IJobExecutionContext context) {
            
        }
    }
}
