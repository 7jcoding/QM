using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AsNum.Common.Extends;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using QM.Shell.Attributes;
using QM.Shell.Common;

namespace QM.Shell.ViewModels {
    /// <summary>
    /// 日历编辑页面
    /// </summary>
    public class EditCalendarViewModel : SchedulerScreen {

        /// <summary>
        /// 支持的日历编辑器类型
        /// <remarks>
        /// Key 日历编辑器类型, Value 日历编辑说明
        /// </remarks>
        /// </summary>
        public static Dictionary<Type, string> EditorTypes;


        private Type selectEditorType = null;
        /// <summary>
        /// 选中的日历编辑器类型
        /// </summary>
        public Type SelectEditorType {
            get {
                return this.selectEditorType;
            }
            set {
                this.selectEditorType = value;
                if (value != null) {
                    this.Editor = (CalendarEditor)Activator.CreateInstance(value);
                    this.NotifyOfPropertyChange(() => this.Editor);
                }
            }
        }

        /// <summary>
        /// 日历编辑器
        /// </summary>
        public CalendarEditor Editor {
            get;
            set;
        }

        /// <summary>
        /// 日历名称
        /// </summary>
        [Required]
        public string CalendarName {
            get;
            set;
        }

        /// <summary>
        /// 日历描述
        /// </summary>
        public string Desc {
            get;
            set;
        }

        /// <summary>
        /// 是否替换现有的日历
        /// </summary>
        public bool ReplaceExists {
            get;
            set;
        }

        /// <summary>
        /// 是否更新 关联的任务中 的日历
        /// </summary>
        public bool UpdateTriggers {
            get;
            set;
        }


        private IScheduler Scheduler;

        static EditCalendarViewModel() {
            var editors = GlobalData.CalendarEditors.Value;//Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsPublic && !t.IsAbstract && typeof(CalendarEditor).IsAssignableFrom(t));
            EditorTypes = new Dictionary<Type, string>();
            foreach (var editor in editors) {
                var desc = editor.GetCustomAttribute<DescriptionAttribute>();
                EditorTypes.Add(editor, desc != null ? desc.Description : editor.Name);
            }
        }

        public EditCalendarViewModel() {
            this.SelectEditorType = EditorTypes.First().Key;
            this.NotifyOfPropertyChange(() => this.SelectEditorType);
        }

        /// <summary>
        /// 用于编辑日历,从其它界面传日历信息到该界面
        /// </summary>
        /// <param name="name">日历名称,因为 ICalendar 中取不到日历名称</param>
        /// <param name="calendar"></param>
        public void SetCalendar(string name, ICalendar calendar) {
            this.CalendarName = name;
            this.Desc = calendar.Description;

            var editorType = GlobalData.CalendarEditors.Value.FirstOrDefault(e => e.GetCustomAttribute<CalendarEditorForAttribute>().CalendarType.Equals(calendar.GetType()));

            this.SelectEditorType = editorType;
            this.Editor.SetCalendar(calendar);
            this.ReplaceExists = true;

            this.NotifyOfPropertyChange(() => this.CalendarName);
            this.NotifyOfPropertyChange(() => this.Desc);
            this.NotifyOfPropertyChange(() => this.SelectEditorType);
            this.NotifyOfPropertyChange(() => this.ReplaceExists);
        }

        public override void Update(IScheduler scheduler) {
            this.Scheduler = scheduler;
        }

        /// <summary>
        /// 保存日历
        /// </summary>
        public void Save() {
            if (string.IsNullOrWhiteSpace(this.CalendarName)) {
                MessageBox.Show("请填写日历名称");
                return;
            }

            if (!this.Editor.IsValid) {
                MessageBox.Show(this.Editor.InvalidMessage);
                return;
            }

            if (!this.ReplaceExists && this.Scheduler.GetCalendar(this.CalendarName) != null) {
                MessageBox.Show("日历已存在，请换个其它名称或选择替换现有日历");
                return;
            }

            var calendar = this.Editor.GetCalendar();
            calendar.Description = this.Desc;
            this.Scheduler.AddCalendar(this.CalendarName, calendar, this.ReplaceExists, this.UpdateTriggers);
        }

        /// <summary>
        /// 返回日历列表界面
        /// </summary>
        public void ShowList() {
            GlobalData.GetShell().ShowAllCalendars();
        }

        public override string Title {
            get {
                return "编辑日历";
            }
        }
    }
}
