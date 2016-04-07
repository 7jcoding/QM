using System;

namespace QM.Server.WebApi.Entity {

    public abstract class BaseScheduleBuilderInfo {
        public abstract ScheduleBuilderTypes Type {
            get;
        }

        public abstract string Summary { get; }
    }

    public class CronScheduleBuilderInfo : BaseScheduleBuilderInfo {
        public string Expression { get; set; }

        public override string Summary {
            get {
                return this.Expression;
            }
        }

        public override ScheduleBuilderTypes Type {
            get {
                return ScheduleBuilderTypes.Cron;
            }
        }
    }

    public class SimpleScheduleBuilderInfo : BaseScheduleBuilderInfo {
        public TimeSpan Interval { get; set; }

        public int RepeatCount { get; set; }

        public override string Summary {
            get {
                return string.Format("Interval:{0} , Repeat:{1}", this.Interval, this.RepeatCount);
            }
        }

        public override ScheduleBuilderTypes Type {
            get {
                return ScheduleBuilderTypes.Simple;
            }
        }
    }

    public enum ScheduleBuilderTypes {
        Simple = 0,
        Cron = 1
    }
}
