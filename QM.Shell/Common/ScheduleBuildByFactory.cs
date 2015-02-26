using QM.Shell.Interfaces;
using QM.Shell.ViewModels;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.Common {
    public static class ScheduleBuildByFactory {

        public static IScheduleBuildByVM Create(ScheduleBuildBy triggerBy) {
            //var types = Assembly.GetExecutingAssembly().GetTypes()
            //    .Where(t => t.IsInstanceOfType(typeof(ITriggerByVM)) );

            switch (triggerBy) {
                case ScheduleBuildBy.Cron:
                    return new CronTriggerViewModel();
                case ScheduleBuildBy.Simple:
                    return new SimpleTriggerViewModel();
                default:
                    return null;
            }
        }

        //public static ITriggerByVM Get(IScheduleBuilder builder) {
        //    var builderType = builder.GetType();
        //    if (builderType.Equals(typeof(SimpleScheduleBuilder))) {
        //        return 
        //    }
        //}

    }
}
