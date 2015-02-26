using Caliburn.Micro;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.Common {

    /// <summary>
    /// 
    /// </summary>
    public abstract class SchedulerScreen : Screen {

        /// <summary>
        /// 窗体标题
        /// </summary>
        public abstract string Title {
            get;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="scheduler"></param>
        public abstract void Update(IScheduler scheduler);

        /// <summary>
        /// 刷新界面
        /// </summary>
        public virtual void Refresh() {
        }

        public override string DisplayName {
            get {
                return this.Title;
            }
        }
    }
}
