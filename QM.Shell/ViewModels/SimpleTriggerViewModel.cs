using Caliburn.Micro;
using QM.Shell.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.ViewModels {

    /// <summary>
    /// SimpleScheduleBuilder 
    /// </summary>
    public class SimpleTriggerViewModel : PropertyChangedBase, IScheduleBuildByVM {

        /// <summary>
        /// 重复次数
        /// </summary>
        public int RepeatCount {
            get;
            set;
        }

        /// <summary>
        /// 执行间隔
        /// </summary>
        public TimeSpan Interval {
            get;
            set;
        }

        public SimpleTriggerViewModel() {
            this.RepeatCount = -1;
        }

        public IScheduleBuilder GetScheduleBuilder() {
            var builder = SimpleScheduleBuilder.Create();
            if (this.RepeatCount < 0)
                builder.RepeatForever();

            builder.WithInterval(this.Interval);
            return builder;
        }

        public string Name {
            get {
                return "简单";
            }
        }


        public Type ApplyTo {
            get {
                return typeof(SimpleScheduleBuilder);
            }
        }


        public void Restore(IScheduleBuilder builder) {
            this.Interval = this.GetField<TimeSpan>("interval", builder);
            this.NotifyOfPropertyChange(() => this.Interval);

            this.RepeatCount = this.GetField<int>("repeatCount", builder);
            this.NotifyOfPropertyChange(() => this.RepeatCount);
        }

        public T GetField<T>(string fieldName, IScheduleBuilder builder) {
            var field = typeof(SimpleScheduleBuilder).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null) {
                return (T)field.GetValue(builder);
            } else
                return default(T);
        }
    }
}
