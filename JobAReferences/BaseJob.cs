using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAReferences {
    public abstract class ThirdPirtyBaseJob : IJob {
        public abstract void Execute(IJobExecutionContext context);
    }
}
