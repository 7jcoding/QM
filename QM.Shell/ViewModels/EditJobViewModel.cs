using Caliburn.Micro;
using Microsoft.Win32;
using QM.Common;
using QM.RemoteLoader;
using QM.Shell.Common;
using QM.Shell.Interfaces;
using Quartz;
using Quartz.Impl.AdoJobStore;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QM.Shell.ViewModels {
    /// <summary>
    /// 任务编辑界面
    /// </summary>
    public class EditJobViewModel : SchedulerScreen {

        private IScheduler Scheduler = null;

        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName {
            get;
            set;
        }

        /// <summary>
        /// 任务组
        /// </summary>
        public string JobGroup {
            get;
            set;
        }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string JobDesc {
            get;
            set;
        }

        /// <summary>
        /// 触发器名称
        /// </summary>
        public string TriggerName {
            get;
            set;
        }

        /// <summary>
        /// 触发器分组
        /// </summary>
        public string TriggerGroup {
            get;
            set;
        }

        /// <summary>
        /// 触发器描述
        /// </summary>
        public string TriggerDesc {
            get;
            set;
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public int TriggerPriority {
            get;
            set;
        }

        private IScheduleBuildByVM scheduleBuildByVM;
        public IScheduleBuildByVM ScheduleBuildByVM {
            get {
                return this.scheduleBuildByVM;
            }
            set {
                this.scheduleBuildByVM = value;
                this.NotifyOfPropertyChange(() => this.ScheduleBuildByVM);
            }
        }

        public List<IScheduleBuildByVM> ScheduleBuildBys {
            get;
            set;
        }

        public BindableCollection<Type> JobTypes {
            get;
            set;
        }


        private Type jobType;
        public Type JobType {
            get {
                return this.jobType;
            }
            set {
                this.jobType = value;
                //this.GetJobParametersFromJobType(value);
                this.Parameters.Clear();
                this.MergeParameters(this.Parameters, value);
            }
        }

        public BindableCollection<Parameter> Parameters {
            get;
            set;
        }

        public List<string> Calendars {
            get;
            set;
        }

        public string ChoicedCalendar {
            get;
            set;
        }


        public bool ReplaceExists {
            get;
            set;
        }

        /// <summary>
        /// 原始的 IScheduleBuilder 类型, 用于编辑
        /// </summary>
        private Type OrgScheduleBuilderType = null;

        public EditJobViewModel() {
            this.TriggerPriority = 5;//默认值
            this.ScheduleBuildBys = GlobalData.ScheduleBuildBys.Value.Select(t => {
                return (IScheduleBuildByVM)Activator.CreateInstance(t);
            }).ToList();

            this.ScheduleBuildByVM = this.ScheduleBuildBys.FirstOrDefault();

            this.NotifyOfPropertyChange(() => this.TriggerPriority);
            this.NotifyOfPropertyChange(() => this.ScheduleBuildByVM);

            this.Parameters = new BindableCollection<Parameter>();
        }

        public void ChoiceDLL() {
            var dialog = new OpenFileDialog() {
                Filter = "DLL|*.dll",
                InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Service", "Jobs")
            };
            if (dialog.ShowDialog() == true) {
                var dll = dialog.FileName;
                try {
                    AppDomain.CurrentDomain.AppendPrivatePath(Path.GetDirectoryName(dll));
                    //不起作用
                    //AppDomain.CurrentDomain.SetupInformation.PrivateBinPathProbe += string.Format(";", Path.GetDirectoryName(dll));

                    //var ald = new AssemblyDynamicLoader(Path.GetDirectoryName(dll));
                    //var loader = ald.GetLoader(dll);
                    //var types = loader.GetTypes();
                    //loader.ExecuteMothod("Fetcher.Soushipping.DestCityFetcherJob", "Test");

                    var asm = Assembly.LoadFrom(dll);
                    this.JobTypes = new BindableCollection<Type>(
                        asm.GetTypes()
                        .Where(t => typeof(IJob).IsAssignableFrom(t)
                            && t.IsPublic
                            && !t.IsAbstract)
                    );
                    this.NotifyOfPropertyChange(() => this.JobTypes);
                    this.JobType = this.JobTypes.FirstOrDefault();
                    this.NotifyOfPropertyChange(() => this.JobType);
                } catch {

                }
            }
        }

        private IEnumerable<Parameter> GetJobParametersFromJobType(Type jobType) {
            var attr = jobType.GetCustomAttribute<ParameterTypeAttribute>(false);
            if (attr != null) {
                var dic = DatamapParser.GetSupportProperties(attr.ParameterType);
                return dic.Select(d => {
                    var desc = d.Key.GetCustomAttribute<DescriptionAttribute>();
                    return new Parameter() {
                        Key = d.Key.Name,
                        Value = d.Value != null ? d.Value.ToString() : null,
                        Desc = desc != null ? desc.Description : d.Key.Name,
                        Type = d.Key.PropertyType
                    };
                });
            }

            return new List<Parameter>();
        }

        private void MergeParameters(IEnumerable<Parameter> ps, Type jobType) {
            List<Parameter> ps2 = new List<Parameter>();
            if (jobType != null) {
                ps2 = this.GetJobParametersFromJobType(jobType).ToList();
                if (ps != null && ps.Count() > 0) {
                    foreach (var p in ps) {
                        var ex = ps2.FirstOrDefault(pp => pp.Key.Equals(p.Key));
                        if (ex != null)
                            ex.Value = p.Value;
                        else
                            ps2.Add(p);
                    }
                }
            }
            this.Parameters = new BindableCollection<Parameter>(ps2);
            this.NotifyOfPropertyChange(() => this.Parameters);
        }

        public override void Update(IScheduler scheduler) {
            this.Scheduler = scheduler;
            this.Calendars = scheduler.GetCalendarNames().ToList();
            if (this.Calendars.Count > 0)
                this.Calendars.Insert(0, "");
        }


        public void SetJob(IScheduler scheduler, ITrigger trigger) {
            this.Update(scheduler);
            var job = this.Scheduler.GetJobDetail(trigger.JobKey);

            this.JobName = trigger.JobKey.Name;
            this.JobGroup = trigger.JobKey.Group;
            this.JobDesc = job.Description;
            this.NotifyOfPropertyChange(() => this.JobName);
            this.NotifyOfPropertyChange(() => this.JobGroup);
            this.NotifyOfPropertyChange(() => this.JobDesc);

            this.TriggerName = trigger.Key.Name;
            this.TriggerGroup = trigger.Key.Group;
            this.TriggerDesc = trigger.Description;
            this.TriggerPriority = trigger.Priority;
            this.NotifyOfPropertyChange(() => this.TriggerName);
            this.NotifyOfPropertyChange(() => this.TriggerGroup);
            this.NotifyOfPropertyChange(() => this.TriggerDesc);
            this.NotifyOfPropertyChange(() => this.TriggerPriority);

            var ps = job.JobDataMap.Select(m => new Parameter() {
                Key = m.Key,
                Value = m.Value != null ? m.Value.ToString() : null
            });

            this.JobTypes = new BindableCollection<Type>();
            this.JobTypes.Add(job.JobType);
            this.JobType = job.JobType;
            this.NotifyOfPropertyChange(() => this.JobTypes);
            this.NotifyOfPropertyChange(() => this.JobType);

            this.MergeParameters(ps, this.jobType);

            this.ChoicedCalendar = trigger.CalendarName;
            this.NotifyOfPropertyChange(() => this.ChoicedCalendar);

            this.ReplaceExists = true;
            this.NotifyOfPropertyChange(() => this.ReplaceExists);


            var builder = trigger.GetScheduleBuilder();
            var builderType = builder.GetType();
            this.ScheduleBuildByVM = this.ScheduleBuildBys.FirstOrDefault(b => b.ApplyTo.Equals(builderType));
            this.ScheduleBuildByVM.Restore(builder);
            this.NotifyOfPropertyChange(() => this.ScheduleBuildByVM);


            #region 如果改变一个已存在的触发器的 IScheduleBuilder 到不同的类型上, RAMStore 是没有问题的, 如果是用 AdoStore 就会有问题
            this.OrgScheduleBuilderType = builderType;
            #endregion
        }

        public void Add() {
            if (this.JobType == null) {
                MessageBox.Show("请选择任务类型");
                return;
            }

            if (!this.ReplaceExists) {
                if (this.Scheduler.CheckExists(new TriggerKey(this.TriggerName, this.TriggerGroup))) {
                    MessageBox.Show("指定的触发器已经存在，请重新指定名称");
                    return;
                }
                if (this.Scheduler.CheckExists(new JobKey(this.JobName, this.JobGroup))) {
                    MessageBox.Show("指定的任务已经存在，请重新指定名称");
                    return;
                }
            }

            try {

                var jobBuilder = JobBuilder.Create(this.JobType)
                    .WithIdentity(this.JobName, this.JobGroup)
                    .WithDescription(this.JobDesc);

                var scheduleBuilder = this.ScheduleBuildByVM.GetScheduleBuilder();
                var triggerBuilder = TriggerBuilder.Create()
                    .WithSchedule(scheduleBuilder)
                    .WithIdentity(this.TriggerName, this.TriggerGroup)
                    .WithPriority(this.TriggerPriority)
                    .WithDescription(this.TriggerDesc);


                var datamap = new JobDataMap();
                foreach (var p in this.Parameters) {
                    datamap.Put(p.Key, p.Value);
                }
                if (datamap.Count > 0)
                    jobBuilder.UsingJobData(datamap);

                if (!string.IsNullOrWhiteSpace(this.ChoicedCalendar))
                    triggerBuilder.ModifiedByCalendar(this.ChoicedCalendar);



                // 如果是用 AdoStore , 修改一个已经存在的 Trigger 的 ScheduleBuilder 到不同的类型,任务会报错,
                // 这里如果是不同的类型,先删除
                var storType = this.Scheduler.GetMetaData().JobStoreType;
                if (storType.Equals(typeof(JobStoreTX)) &&
                    this.OrgScheduleBuilderType != null &&
                    !this.OrgScheduleBuilderType.Equals(scheduleBuilder.GetType())
                   ) {
                    var key = new JobKey(this.JobName, this.JobGroup);
                    this.Scheduler.DeleteJob(key);
                }


                if (this.ReplaceExists) {
                    var triggers = new Quartz.Collection.HashSet<ITrigger>();
                    triggers.Add(triggerBuilder.Build());

                    this.Scheduler.ScheduleJob(jobBuilder.Build(), triggers, true);
                } else {
                    var jd = jobBuilder.Build();
                    var t = triggerBuilder.Build();
                    this.Scheduler.ScheduleJob(jd, t);
                    //this.Scheduler.ScheduleJob(jobBuilder.Build(), triggerBuilder.Build());
                }

                MessageBox.Show("保存成功");
            } catch (Exception ex) {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }
        public override string Title {
            get {
                return "任务编辑";
            }
        }
    }

    public class Parameter {
        public string Key {
            get;
            set;
        }

        public string Value {
            get;
            set;
        }

        public string Desc {
            get;
            set;
        }

        public Type Type {
            get;
            set;
        }
    }
}
