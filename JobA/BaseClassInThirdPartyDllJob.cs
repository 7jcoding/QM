using JobAReferences;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobA {
    /// <summary>
    /// BaseJob in JobAReferences.dll, and not included in QM.Server, 
    /// So, can't add this job to Quartz, except place JobAReferences.dll into QM.Server\Jobs
    /// Or add references to QM.Server
    /// </summary>
    [Description("基类在第三方DLL中,加载失败的示例")]
    public class BaseClassInThirdPartyDllJob : BaseJob {
        public override void Execute(Quartz.IJobExecutionContext context) {

        }
    }
}
