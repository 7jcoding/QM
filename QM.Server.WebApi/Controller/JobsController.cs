using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QM.Server.WebApi.Controller {
    public class JobsController : ApiController {

        public IEnumerable<IJobDetail> Get() {
            var scd = StdSchedulerFactory.GetDefaultScheduler();
            var grps = scd.GetJobGroupNames();
            var jobs = new List<IJobDetail>(grps.SelectMany(grp => scd.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(grp)))
                .Select(key => scd.GetJobDetail(key)));

            return jobs;
        }

    }
}
