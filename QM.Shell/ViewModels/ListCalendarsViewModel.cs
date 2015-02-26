using Caliburn.Micro;
using Quartz;
using Quartz.Impl.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AsNum.WPF.Controls;
using System.Windows.Threading;
using System.Windows;
using QM.Shell.Attributes;
using QM.Shell.Common;

namespace QM.Shell.ViewModels {

    /// <summary>
    /// Calendars 列表
    /// </summary>
    public class ListCalendarsViewModel : SchedulerScreen {

        /// <summary>
        /// 当前所有的日历
        /// <remarks>Key 日历名称, Value 日历</remarks>
        /// </summary>
        public Dictionary<string, ICalendar> Calendars {
            get;
            set;
        }


        private KeyValuePair<string, ICalendar> currentCalendar;
        /// <summary>
        /// 选中的日历
        /// <remarks>Key 日历名称, Value 日历</remarks>
        /// </summary>
        public KeyValuePair<string, ICalendar> CurrentCalendar {
            get {
                return this.currentCalendar;
            }
            set {
                this.currentCalendar = value;
                if (value.Value != null) {
                    this.CanEdit = true;
                    this.LoadEditor(value.Value.GetType());
                } else {
                    this.LoadEditor(null);
                    this.CanEdit = false;
                }
                this.NotifyOfPropertyChange(() => this.CanEdit);

            }
        }

        /// <summary>
        /// 日历编辑器
        /// </summary>
        public CalendarEditor Editor {
            get;
            set;
        }

        public bool CanEdit {
            get;
            set;
        }

        private IScheduler Scheduler;

        /// <summary>
        /// 加载日历编辑器
        /// </summary>
        /// <param name="calendarType"></param>
        private void LoadEditor(Type calendarType) {
            if (calendarType != null) {

                var editorType = GlobalData.CalendarEditors.Value
                    .FirstOrDefault(t =>
                        (t.GetCustomAttribute<CalendarEditorForAttribute>()
                        ?? new CalendarEditorForAttribute(null))
                        .CalendarType.Equals(calendarType));

                if (editorType != null) {
                    this.Editor = (CalendarEditor)Activator.CreateInstance(editorType);
                    this.Editor.SetCalendar(this.CurrentCalendar.Value);
                }
            } else
                this.Editor = null;

            this.NotifyOfPropertyChange(() => this.Editor);
        }

        public override void Update(Quartz.IScheduler scheduler) {
            this.Scheduler = scheduler;

            this.Calendars = scheduler.GetCalendarNames().Select(n => new {
                Name = n,
                Calendar = scheduler.GetCalendar(n)
            }).ToDictionary(n => n.Name, n => n.Calendar);

            this.NotifyOfPropertyChange(() => this.Calendars);
        }

        public override void Refresh() {
            this.Update(this.Scheduler);
        }

        /// <summary>
        /// 调用编辑界面
        /// </summary>
        public void Edit() {
            var shell = GlobalData.GetShell();
            var editor = new EditCalendarViewModel();
            editor.SetCalendar(this.CurrentCalendar.Key, this.CurrentCalendar.Value);
            shell.ShowDialog(editor, 700, 500);
        }

        public void Delete() {
            if (MessageBox.Show("确认要删除日历？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                if (this.Scheduler.DeleteCalendar(this.CurrentCalendar.Key)) {
                    this.Refresh();
                    this.CurrentCalendar = new KeyValuePair<string, ICalendar>();
                    this.NotifyOfPropertyChange(() => this.CurrentCalendar);
                } else {
                    MessageBox.Show("删除失败");
                }
            }
        }

        public override string Title {
            get {
                return "日历列表";
            }
        }
    }
}
