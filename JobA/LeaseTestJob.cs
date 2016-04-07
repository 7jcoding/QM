using Quartz;
using System;
using System.ComponentModel;
using System.Threading;

namespace JobA {

    [Description("续租测试")]
    public class LeaseTestJob : IJob {
        public void Execute(IJobExecutionContext context) {
            Thread.Sleep(TimeSpan.FromMinutes(11));
        }
    }
}
