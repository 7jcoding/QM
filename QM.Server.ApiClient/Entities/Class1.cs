using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.ApiClient.Entities {

    public class Trigger {
        public Cronex cronEx { get; set; }
        public DateTime startTimeUtc { get; set; }
        public object endTimeUtc { get; set; }
        public DateTime nextFireTimeUtc { get; set; }
        public object previousFireTimeUtc { get; set; }
        public string name { get; set; }
        public string group { get; set; }
        public string jobName { get; set; }
        public string jobGroup { get; set; }
        public string description { get; set; }
        public object jobDataMap { get; set; }
        public object calendarName { get; set; }
        public object fireInstanceId { get; set; }
        public int misfireInstruction { get; set; }
        public int priority { get; set; }
    }

    public class Cronex {
        public string cronExpressionString { get; set; }
        public Timezone timeZone { get; set; }
    }

    public class Timezone {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string StandardName { get; set; }
        public string DaylightName { get; set; }
        public string BaseUtcOffset { get; set; }
        public object AdjustmentRules { get; set; }
        public bool SupportsDaylightSavingTime { get; set; }
    }

}
