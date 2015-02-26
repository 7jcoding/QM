using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server {
    public class IsolatedJobFactory : IJobFactory {

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler) {
            return NewJob(bundle.JobDetail.JobType);
        }

        private IJob NewJob(Type jobType) {
            return new IsolatedJob(jobType);
        }

        public void ReturnJob(IJob job) {
            IDisposable disposable = job as IDisposable;
            if (disposable != null) {
                disposable.Dispose();
            }
        }
    }
}
