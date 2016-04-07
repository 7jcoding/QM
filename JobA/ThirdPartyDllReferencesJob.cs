using JobAReferences;
using Quartz;
using System;
using System.ComponentModel;

namespace JobA {

    [Description("第三方DLL引用示例")]
    public class ThirdPartyDllReferencesJob : IJob {
        public void Execute(IJobExecutionContext context) {
            var test = new Test();
            Console.WriteLine(test.Add(1000, 10000));
        }
    }
}
