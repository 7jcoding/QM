using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.Common {
    /// <summary>
    /// REMOTING 连接
    /// </summary>
    public class Connection {

        public string Host { get; set; }

        public int Port { get; set; }

        public string BindName { get; set; }

        public static IScheduler Scheduler = null;

        public Connection() {
            this.Host = "localhost";
            this.Port = 5555;
            this.BindName = "TestScheduler";
        }

        public override string ToString() {
            return string.Format("tcp://{0}:{1}/{2}", this.Host, this.Port, this.BindName);
        }

        public static bool Connect(Connection conn) {
            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "RemoteClient";
            properties["quartz.scheduler.proxy"] = "true";
            properties["quartz.threadPool.threadCount"] = "0";
            properties["quartz.scheduler.proxy.address"] = conn.ToString();
            try {
                var factory = new StdSchedulerFactory(properties);
                var scheduler = factory.GetScheduler();
                Scheduler = scheduler;
                return true;
            } catch {
                return false;
            }
        }
    }
}
