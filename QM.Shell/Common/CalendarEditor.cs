using Caliburn.Micro;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.Common {

    /// <summary>
    /// 日历编辑器基类
    /// </summary>
    public abstract class CalendarEditor : PropertyChangedBase {

        /// <summary>
        /// 获取日历
        /// </summary>
        /// <returns></returns>
        public abstract ICalendar GetCalendar();


        /// <summary>
        /// 日历是否有效
        /// </summary>
        public abstract bool IsValid {
            get;
        }

        /// <summary>
        /// 日历无效时原因
        /// </summary>
        public abstract string InvalidMessage {
            get;
        }

        /// <summary>
        /// 用于编辑日历, 向日历编辑界面提供数据
        /// </summary>
        /// <param name="calendar"></param>
        public abstract void SetCalendar(ICalendar calendar);
    }
}
