using Caliburn.Micro;
using QM.Manager.Common;
using QM.Server.ApiClient;
using QM.Server.ApiClient.Methods;
using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QM.Manager.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class JobEditorViewModel : BaseScreen {
        public override string Title {
            get {
                return "任务编辑";
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// 分组
        /// </summary>
        private string Group {
            get; set;
        }

        ///// <summary>
        ///// 描述
        ///// </summary>
        //public string Desc { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public BindableCollection<JobParameterInfo> DataMap { get; set; }

        ///// <summary>
        ///// 是否可恢复
        ///// </summary>
        //public bool ShouldRecover { get; set; }

        //public bool Durability { get; set; }

        ///// <summary>
        ///// 是否替换已存在的
        ///// </summary>
        //public bool Replace { get; set; }

        public JobInfo Data { get; set; }

        /// <summary>
        /// 可选的任务类型
        /// </summary>
        public IEnumerable<TypeInfo> Types { get; set; }

        private TypeInfo _currentType = null;
        /// <summary>
        /// 当前选择的任务类型
        /// </summary>
        public TypeInfo CurrentType {
            get {
                return this._currentType;
            }
            set {
                this._currentType = value;
                this.DataMap.Clear();
                if (value != null) {
                    this.DataMap.AddRange(value.Params);
                }
            }
        }




        private SimpleContainer Container = null;
        private IEventAggregator EventAggregator = null;

        private ChoiceDllViewModel ChoiceDllVM = null;

        public JobEditorViewModel(SimpleContainer container, IEventAggregator eag) {
            this.Container = container;
            this.EventAggregator = eag;
            this.EventAggregator.Subscribe(this);

            this.ChoiceDllVM = this.Container.GetInstance<ChoiceDllViewModel>();
            this.ChoiceDllVM.OnChoice += ChoiceDllVM_OnChoice;

            this.DataMap = new BindableCollection<JobParameterInfo>();
        }

        private async void ChoiceDllVM_OnChoice(object sender, ChoiceDllViewModel.ChoiceArgs e) {
            var mth = new GetJobTypes() {
                DllPath = e.Dll
            };
            this.Types = await ApiClient.Instance.Execute(mth);
            this.CurrentType = this.Types.FirstOrDefault();
            this.NotifyOfPropertyChange(() => this.Types);
            this.NotifyOfPropertyChange(() => this.CurrentType);
            await this.EventAggregator.PublishOnUIThreadAsync(new HideDialogRequest());
        }

        public async override Task Update() {
            if (string.IsNullOrWhiteSpace(this.Name)) {
                this.Data = new JobInfo();
            } else {
                // Get Job Data
                var mth = new GetJob() {
                    Group = this.Group,
                    Name = this.Name
                };
                this.Data = await ApiClient.Instance.Execute(mth);

                // Load Types from job's dll
                var mth2 = new GetJobTypes() {
                    DllPath = this.Data.JobType.AssemblyFile
                };
                this.Types = await ApiClient.Instance.Execute(mth2);
                this.CurrentType = this.Types.FirstOrDefault(t => t.FullName.Equals(this.Data.JobType.FullName));

                //Merge Parameter
                foreach (var kv in this.Data.DataMap) {
                    var t = this.DataMap.FirstOrDefault(d => d.Name.Equals(kv.Key));
                    if (t != null)
                        t.Value = kv.Value;
                }
            }
            this.NotifyOfPropertyChange(() => this.Data);
            this.NotifyOfPropertyChange(() => this.Types);
            this.NotifyOfPropertyChange(() => this.CurrentType);
        }

        public async Task Update(string name, string group) {
            this.Name = name;
            this.Group = group;
            await this.Update();
        }

        public async void Upload() {
            var vm = this.Container.GetInstance<UploadViewModel>();
            await this.EventAggregator.PublishOnUIThreadAsync(new OpenRequest() {
                VM = vm,
                OpenAsDialog = true,
                Height = 150,
                Width = 500
            });
        }

        public async void ChoiceDll() {
            await this.EventAggregator.PublishOnUIThreadAsync(new OpenRequest() {
                VM = this.ChoiceDllVM,
                OpenAsDialog = true
            });
            await this.ChoiceDllVM.Update();
        }

        public async void Save() {
            var mth = new SaveJob(this.CurrentType.FullName, this.CurrentType.AssemblyFile) {
                Desc = this.Data.Desc,
                Durability = this.Data.Durability,
                Name = this.Data.Name,
                Group = this.Data.Group,
                Parameters = this.DataMap.ToDictionary(p => p.Name, p => p.Value),
                ReplaceExists = this.Data.ReplaceExists,
                ShouldRecover = this.Data.ShouldRecover
            };
            var result = await ApiClient.Instance.Execute(mth);
            MessageBox.Show(result.ToString());
        }
    }
}
