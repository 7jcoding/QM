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
            string jobType = null;
            string jobDesc = null;
            Dictionary<string, object> jobDataMap = null;
            try {
                var job = scd.GetJobDetail(t.JobKey);
                jobDataMap = job.JobDataMap.ToDictionary(d => d.Key, d => d.Value);
                var type = job.JobType;
                jobType = type.AssemblyQualifiedName;
                var attr = type.GetCustomAttribute<DescriptionAttribute>();
                if (attr != null)
                    jobDesc = attr.Description;

            } catch {
                //如果发生异常，可能是以下原因：
                //数据库中有的任务，在 Jobs 目录下并不存在
                return null;
            }
            return new TriggerInfo() {
                Calendar = t.CalendarName,
                Desc = t.Description,
                EndTime = t.EndTimeUtc,
                FinalFireTime = t.FinalFireTimeUtc,
                HasMillisecondPrecision = t.HasMillisecondPrecision,
                TriggerDataMap = t.JobDataMap.ToDictionary(d => d.Key, d => d.Value),
                JobDataMap = jobDataMap,
                JobGroup = t.JobKey.Group,
                JobName = t.JobKey.Name,
                MisfireInstruction = t.MisfireInstruction,
                Priority = t.Priority,
                StartTime = t.StartTimeUtc,
                TriggerGroup = t.Key.Group,
                TriggerName = t.Key.Name,
                NextFireTime = t.GetNextFireTimeUtc(),
                PreviousFireTime = t.GetPreviousFireTimeUtc(),
                JobType = jobType,
                JobDesc = jobDesc,
                State = (Entity.TriggerState)(int)scd.GetTriggerState(t.Key)
            };
        }
    }
}
