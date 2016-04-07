using Quartz;
using System;
using System.ComponentModel;
using System.Configuration;

namespace JobA {

    [Description("配置文件读取示例")]
    public class AppConfigReadJob : IJob {
        public void Execute(IJobExecutionContext context) {
            Console.WriteLine(ConfigurationManager.AppSettings.Get("TTT"));
        }
    }
}
