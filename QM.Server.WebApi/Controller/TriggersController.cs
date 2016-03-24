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
using System.Web.Http.Cors;
using System.Reflection;
using System.ComponentModel;

namespace QM.Server.WebApi.Controller {

    public class TriggersController : ApiController {

        public IEnumerable<TriggerInfo> Get() {
            var scd = StdSchedulerFactory.GetDefaultScheduler();
            var grps = scd.GetTriggerGroupNames();
            var tks = grps.SelectMany(grp => scd.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(grp)));
            return tks.Select(k => {
                var t = scd.GetTrigger(k);
                return Convert(t, scd);
            }).Where(t => t != null);
        }

        [HttpGet]
        public TriggerInfo Get(string group, string name) {
            var scd = StdSchedulerFactory.GetDefaultScheduler();
            var t = scd.GetTrigger(new TriggerKey(name, group));
            if (t != null) {
                return Convert(t, scd);
            } else
                return null;
        }


        private TriggerInfo Convert(ITrigger t, IScheduler scd) {
            var job = scd.GetJobDetail(t.JobKey);

            return new TriggerInfo() {
                Calendar = t.CalendarName,
                Desc = t.Description,
                EndTime = t.EndTimeUtc,
                FinalFireTime = t.FinalFireTimeUtc,
                HasMillisecondPrecision = t.HasMillisecondPrecision,
                TriggerDataMap = t.JobDataMap.ToDictionary(d => d.Key, d => d.Value),
                JobDataMap = job.JobDataMap.ToDictionary(d => d.Key, d => d.Value),
                JobGroup = t.JobKey.Group,
                JobName = t.JobKey.Name,
                MisfireInstruction = t.MisfireInstruction,
                Priority = t.Priority,
                StartTime = t.StartTimeUtc,
                TriggerGroup = t.Key.Group,
                TriggerName = t.Key.Name,
                NextFireTime = t.GetNextFireTimeUtc(),
                PreviousFireTime = t.GetPreviousFireTimeUtc(),
                JobType = new JobType(job.JobType),
                JobDesc = job.Description,
                State = (Entity.TriggerState)(int)scd.GetTriggerState(t.Key),
                ScheduleBuilderInfo = t.GetScheduleBuilder().GetInfo()
            };
        }

        [HttpPut]
        public TriggerSaveState Put([FromBody]TriggerInfo trigger) {
            var scd = StdSchedulerFactory.GetDefaultScheduler();

            var sb = trigger.ScheduleBuilderInfo.Build();
            if (sb == null)
                return TriggerSaveState.NotSupportScheduleBuilder;

            var job = scd.GetJobDetail(new JobKey(trigger.JobName, trigger.JobGroup));
            var tb = TriggerBuilder.Create()
                        .ForJob(job)
                        .WithSchedule(sb)
                        .WithDescription(trigger.Desc)
                        .WithPriority(trigger.Priority)
                        .WithIdentity(trigger.TriggerName, trigger.TriggerGroup);

            scd.ScheduleJob(tb.Build());
            return TriggerSaveState.Success;
        }
    }
}
