using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QM.Server.WebApi.Controller {

    [EnableCors("*", "*", "*")]
    public class TriggersController : ApiController {

        public IEnumerable<ITrigger> Get() {
            var scd = StdSchedulerFactory.GetDefaultScheduler();
            var grps = scd.GetTriggerGroupNames();
            var tks = grps.SelectMany(grp => scd.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(grp)));
            return tks.Select(k => {
                return scd.GetTrigger(k);
            });
        }

    }
}
