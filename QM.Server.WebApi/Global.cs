using Quartz;
using Quartz.Impl;
using System;

namespace QM.Server.WebApi {
    public static class Global {

        private static readonly Lazy<IScheduler> _Scheduler = new Lazy<IScheduler>(() => {
            return StdSchedulerFactory.GetDefaultScheduler();
        });

        public static IScheduler Scheduler {
            get {
                return _Scheduler.Value;
            }
        }
    }
}
