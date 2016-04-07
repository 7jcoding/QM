using Quartz;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QM.Server.WebApi.Controller {
    [EnableCors("*", "*", "*")]
    public class MetadataController : ApiController {
        public SchedulerMetaData Get() {
            var scd = Global.Scheduler;
            return scd.GetMetaData();
        }
    }
}
