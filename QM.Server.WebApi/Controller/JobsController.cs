using QM.Server.WebApi.Entity;
using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace QM.Server.WebApi.Controller {
    public class JobsController : ApiController {

        [HttpGet]
        public IEnumerable<JobInfo> Get() {
            var scd = Global.Scheduler;
            var grps = scd.GetJobGroupNames();
            var jobs = grps.SelectMany(
                            grp => scd.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(grp))
                       )
                       .Select(key => Convert(scd.GetJobDetail(key)));

            return jobs;
        }

        [HttpGet]
        public JobInfo Get(string name, string group) {
            var scd = Global.Scheduler;
            var job = scd.GetJobDetail(new JobKey(name, group));
            return this.Convert(job);
        }


        private JobInfo Convert(IJobDetail j) {
            if (j == null)
                return null;

            return new JobInfo() {
                DataMap = j.JobDataMap.ToDictionary(d => d.Key, d => d.Value),
                Desc = j.Description,
                Durability = j.Durable,
                Group = j.Key.Group,
                JobType = new JobType(j.JobType),
                Name = j.Key.Name,
                ShouldRecover = j.RequestsRecovery
            };
        }

        [HttpPut]
        public JobSaveStates Put([FromBody]JobInfo job) {
            var scd = Global.Scheduler;
            if (!job.ReplaceExists && scd.CheckExists(new JobKey(job.Name, job.Group))) {
                return JobSaveStates.JobExists;
            }

            var o = Activator.CreateInstanceFrom(job.JobType.AssemblyFile, job.JobType.FullName)
                .Unwrap();
            Type jobType = o.GetType();
            var builder = JobBuilder.Create(jobType)
                .RequestRecovery(job.ShouldRecover)
                .SetJobData(new JobDataMap(job.DataMap as IDictionary<string, object>))
                .StoreDurably(job.Durability)
                .WithDescription(job.Desc)
                .WithIdentity(new JobKey(job.Name, job.Group));

            scd.AddJob(builder.Build(), job.ReplaceExists, true);

            return JobSaveStates.Success;
        }

        [HttpDelete]
        public bool Delete(string name, string group = null) {
            var scd = Global.Scheduler;
            return scd.DeleteJob(new JobKey(name, group));
        }
    }
}
