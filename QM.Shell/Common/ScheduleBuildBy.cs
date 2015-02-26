using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.Common {

    /// <summary>
    /// 支持的触发器类型,
    /// </summary>
    public enum ScheduleBuildBy {
        /// <summary>
        /// 简单的 SimpleScheduleBuilder
        /// </summary>
        Simple,
        /// <summary>
        /// Corn 表达式
        /// </summary>
        Cron
    }
}
