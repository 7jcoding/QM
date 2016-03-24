using QM.Server.WebApi.Entity;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.WebApi {
    public static class ScheduleBuilderHelper {
        #region
        private static Dictionary<Type, Func<IScheduleBuilder, BaseScheduleBuilderInfo>> GeterDic =
            new Dictionary<Type, Func<IScheduleBuilder, BaseScheduleBuilderInfo>>() {
            {
                typeof(CronScheduleBuilder),
                (builder)=> {
                    var expr = GetField<CronExpression>("cronExpression", builder);
                    if (expr != null) {
                        var expression = expr.CronExpressionString;
                        return new CronScheduleBuilderInfo() {
                             Expression = expression
                        };
                    }

                    return null;
                }
            },

            {
                typeof(SimpleScheduleBuilder),
                builder => {
                    var interval = GetField<TimeSpan>("interval", builder);
                    var repeatCount = GetField<int>("repeatCount", builder);

                    return new SimpleScheduleBuilderInfo() {
                         Interval = interval,
                         RepeatCount = repeatCount
                    };
                }
            }
        };
        #endregion

        #region
        private static Dictionary<Type, Func<BaseScheduleBuilderInfo, IScheduleBuilder>> BuilderDic =
            new Dictionary<Type, Func<BaseScheduleBuilderInfo, IScheduleBuilder>>() {
                {
                    typeof(SimpleScheduleBuilderInfo),
                    info=> {
                        var i = (SimpleScheduleBuilderInfo)info;
                        return SimpleScheduleBuilder.Create()
                        .WithInterval(i.Interval)
                        .WithRepeatCount(i.RepeatCount);
                    }
                },
                {
                    typeof(CronScheduleBuilderInfo),
                    info => {
                        var i = (CronScheduleBuilderInfo)info;
                        return CronScheduleBuilder.CronSchedule(i.Expression);
                    }
                }
            };
        #endregion

        public static BaseScheduleBuilderInfo GetInfo(this IScheduleBuilder builder) {
            var type = builder.GetType();
            if (GeterDic.ContainsKey(type)) {
                return GeterDic[type](builder);
            } else
                return null;
        }

        public static IScheduleBuilder Build(this BaseScheduleBuilderInfo info) {
            var t = info.GetType();
            if (BuilderDic.ContainsKey(t)) {
                return BuilderDic[t](info);
            } else
                return null;
        }

        public static T GetField<T>(string fieldName, IScheduleBuilder builder) {
            var field = typeof(SimpleScheduleBuilder).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null) {
                return (T)field.GetValue(builder);
            } else
                return default(T);
        }
    }
}
