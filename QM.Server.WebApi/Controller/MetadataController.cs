using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QM.Server.WebApi.Controller {
    [EnableCors("*", "*", "*")]
    public class MetadataController : ApiController {
        public SchedulerMetaData Get() {
            var scd = StdSchedulerFactory.GetDefaultScheduler();
            return scd.GetMetaData();
        }
    }
}
