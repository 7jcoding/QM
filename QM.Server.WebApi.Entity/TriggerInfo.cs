using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.WebApi.Entity {
    public class TriggerInfo {
        public string Calendar { get; set; }

        public string Desc { get; set; }

        public object EndTime { get; set; }

        public DateTimeOffset? FinalFireTime { get; set; }

        public bool HasMillisecondPrecision { get; set; }

        public Dictionary<string, object> TriggerDataMap { get; set; }

        public Dictionary<string, object> JobDataMap { get; set; }

        public string JobName { get; set; }

        public string JobGroup { get; set; }

        public string TriggerName { get; set; }

        public string TriggerGroup { get; set; }

        public int MisfireInstruction { get; set; }

        public int Priority { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset? NextFireTime { get; set; }

        public DateTimeOffset? PreviousFireTime { get; set; }
        //public string JobType { get; set; }
        public JobType JobType { get; set; }

        public string JobDesc { get; set; }

        public TriggerState State { get; set; }

        public BaseScheduleBuilderInfo ScheduleBuilderInfo { get; set; }
    }
}
