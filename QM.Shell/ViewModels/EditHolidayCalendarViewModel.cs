using Caliburn.Micro;
using QM.Shell.Attributes;
using QM.Shell.Common;
using Quartz;
using Quartz.Impl.Calendar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.ViewModels {

    /// <summary>
    /// 节假日日历编辑界面, 应用 HoliadyCalendar
    /// </summary>
    [Description("节假日日历"), CalendarEditorFor(typeof(HolidayCalendar))]
    public class EditHolidayCalendarViewModel : CalendarEditor {

        /// <summary>
        /// 节假日列表,节假日不会执行任务
        /// </summary>
        public BindableCollection<DateTime> Dates {
            get;
            set;
        }

        public EditHolidayCalendarViewModel() {
            this.Dates = new BindableCollection<DateTime>();
            this.WillAddDate = DateTime.Now;
        }

        /// <summary>
        /// 即将添加到日历中的日期
        /// </summary>
        public DateTime WillAddDate {
            get;
            set;
        }

        public void Add() {
            this.Dates.Add(this.WillAddDate);
            this.NotifyOfPropertyChange(() => this.Dates);
        }

        public override ICalendar GetCalendar() {
            var cal = new HolidayCalendar();
            cal.TimeZone = TimeZoneInfo.Local;

            foreach (var d in this.Dates) {
                cal.AddExcludedDate(d);
            }
            return cal;
        }

        /// <summary>
        /// 验证日历是否有效
        /// </summary>
        public override bool IsValid {
            get {
                return this.Dates.Count > 0;
            }
        }

        /// <summary>
        /// 日历无效时的具体信息
        /// </summary>
        public override string InvalidMessage {
            get {
                return "请添加排除日期";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="calendar"></param>
        public override void SetCalendar(ICalendar calendar) {
            var cal = (HolidayCalendar)calendar;
            if (cal == null)
                return;

            this.Dates = new BindableCollection<DateTime>(cal.ExcludedDates);
            this.NotifyOfPropertyChange(() => this.Dates);
        }
    }
}
