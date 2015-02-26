using Common.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Topshelf;

namespace QM.Server {
    class QMServer : ServiceControl {

        private ISchedulerFactory Factory = null;
        private IScheduler Scheduler = null;

        private bool CanStart = false;
        public QMServer() {
            try {
                this.Factory = new StdSchedulerFactory();
                this.Scheduler = this.Factory.GetScheduler();
                this.Scheduler.JobFactory = new IsolatedJobFactory();
                this.CanStart = true;
            } catch {
                this.CanStart = false;
            }
        }

        public bool Start(HostControl hostControl) {
            if (this.Scheduler != null) {
                this.LoadSchedulerListener(this.Scheduler);
                this.Scheduler.Start();
            }

            this.LoadTriggerListeners(this.Scheduler);

            return this.CanStart;
        }

        public bool Stop(HostControl hostControl) {
            if (this.Scheduler != null) {
                this.Scheduler.Shutdown();
            }
            return true;
        }

        private void LoadTriggerListeners(IScheduler scheduler) {
            var udp = new UDPListener.UDPTriggerListener();
            scheduler.ListenerManager.AddTriggerListener(udp, GroupMatcher<TriggerKey>.AnyGroup());
        }

        private void LoadSchedulerListener(IScheduler scheduler) {
            var udp = new UDPListener.UDPSchedulerListener();
            scheduler.ListenerManager.AddSchedulerListener(udp);
        }
    }
}
