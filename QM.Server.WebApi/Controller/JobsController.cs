using QM.Server.WebApi.Entity;
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

        public IEnumerable<JobInfo> Get() {
            var scd = StdSchedulerFactory.GetDefaultScheduler();
            var grps = scd.GetJobGroupNames();
            var jobs = grps.SelectMany(
                            grp => scd.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(grp))
                       )
                       .Select(key => Convert(scd.GetJobDetail(key)));

            return jobs;
        }


        private JobInfo Convert(IJobDetail j) {
            return new JobInfo() {
                DataMap = j.JobDataMap.ToDictionary(d => d.Key, d => d.Value),
                Desc = j.Description,
                Durability = j.Durable,
                Group = j.Key.Group,
                JobType = j.JobType.AssemblyQualifiedName,
                Name = j.Key.Name,
                ShouldRecover = j.RequestsRecovery
            };
        }
    }
}
