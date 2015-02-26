using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace JobB {
    public class Job : MarshalByRefObject, IJob {
        public void Execute(IJobExecutionContext context) {

            var evidence = new Evidence();
            var setup = new AppDomainSetup();

            var domain = AppDomain.CreateDomain("JobB", evidence, setup);

            var t = ConfigurationManager.AppSettings.Get("TTT");
            Console.WriteLine(t);

            Console.WriteLine("JobB {0}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }
    }
}
