using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JobA {

    [Description("续租测试")]
    public class LeaseTestJob : IJob {
        public void Execute(IJobExecutionContext context) {
            Thread.Sleep(TimeSpan.FromMinutes(11));
        }
    }
}
