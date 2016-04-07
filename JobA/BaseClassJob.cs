using Quartz;
using System.ComponentModel;

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
