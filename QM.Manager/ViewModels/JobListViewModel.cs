using Caliburn.Micro;
using Microsoft.Win32;
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
    public class JobListViewModel : BaseScreen {
        public override string Title {
            get {
                return "任务列表";
            }
        }

        public IEnumerable<JobInfo> Datas { get; set; }

        private JobInfo _current = null;
        public JobInfo Current {
            get {
                return this._current;
            }
            set {
                this._current = value;
                this.DataMapVM.Update(value != null ? value.DataMap : null);
            }
        }

        public DataMapViewModel DataMapVM { get; set; }

        private SimpleContainer Container = null;

        private IEventAggregator EventAggregator = null;

        public JobListViewModel(SimpleContainer container, IEventAggregator eag) {
            this.Container = container;
            this.EventAggregator = eag;
            this.EventAggregator.Subscribe(this);

            this.DataMapVM = new DataMapViewModel();
        }

        public async override Task Update() {
            var mth = new GetJobs();
            this.Datas = await ApiClient.Instance.Execute(mth);
            this.NotifyOfPropertyChange(() => this.Datas);
        }

        public async void Add() {
            var vm = this.Container.GetInstance<JobEditorViewModel>();
            await this.EventAggregator.PublishOnUIThreadAsync(new OpenRequest() {
                VM = vm
            });
        }

        public async void Edit() {
            var vm = this.Container.GetInstance<JobEditorViewModel>();
            await this.EventAggregator.PublishOnUIThreadAsync(new OpenRequest() {
                VM = vm
            });
            await vm.Update(this.Current.Name, this.Current.Group);
        }

        public async void Delete() {
            if (MessageBox.Show("确认要删除该任务吗？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                var mth = new DeleteJob() {
                    Name = this.Current.Name,
                    Group = this.Current.Group
                };
                var result = await ApiClient.Instance.Execute(mth);

                if (result)
                    await this.Update();
                else
                    MessageBox.Show("删除失败");
            }
        }

        public async void Trigger() {
            var vm = this.Container.GetInstance<TriggerEditorViewModel>();
            await this.EventAggregator.PublishOnUIThreadAsync(new OpenRequest() {
                VM = vm
            });
            vm.Update(this.Current);
        }
    }
}
