using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobA {

    [Description("配置文件读取示例")]
    public class AppConfigReadJob : IJob {
        public void Execute(IJobExecutionContext context) {
            Console.WriteLine(ConfigurationManager.AppSettings.Get("TTT"));
        }
    }
}
