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
    /// Corn 表达式编辑器
    /// </summary>
    public class CronTriggerViewModel : PropertyChangedBase, IScheduleBuildByVM {
        public string Expression {
            get;
            set;
        }

        public IScheduleBuilder GetScheduleBuilder() {
            return CronScheduleBuilder.CronSchedule(this.Expression);
        }

        public string Name {
            get {
                return "Corn 表达式";
            }
        }


        public Type ApplyTo {
            get {
                return typeof(CronScheduleBuilder);
            }
        }


        public void Restore(IScheduleBuilder builder) {
            var expr = this.GetField<CronExpression>("cronExpression", builder);
            if (expr != null) {
                this.Expression = expr.CronExpressionString;
                this.NotifyOfPropertyChange(() => this.Expression);
            }
        }

        public T GetField<T>(string fieldName, IScheduleBuilder builder) {
            var field = typeof(CronScheduleBuilder).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null) {
                return (T)field.GetValue(builder);
            } else
                return default(T);
        }
    }
}
