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

        public async void Upload() {
            var vm = this.Container.GetInstance<UploadViewModel>();
            await this.EventAggregator.PublishOnUIThreadAsync(new OpenDialogRequest() {
                VM = vm,
                Height = 150,
                Width = 500
            });
        }

        public async void Add() {
            var vm = this.Container.GetInstance<JobEditorViewModel>();
            await this.EventAggregator.PublishOnUIThreadAsync(new OpenDialogRequest() {
                VM = vm,
                Height = 500,
                Width = 700
            });
        }
    }
}
