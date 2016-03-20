using Caliburn.Micro;
using QM.Manager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Manager.ViewModels {
    public class ShellViewModel : Screen, IShell, IHandle<OpenRequest> {

        public BaseScreen CurrentVM {
            get;
            set;
        }

        public BaseScreen DialogVM {
            get;
            set;
        }

        public bool IsShowDialog {
            get;
            set;
        }

        public string DialogTitle {
            get;
            set;
        }

        public int DialogWidth {
            get;
            set;
        }

        public int DialogHeight {
            get;
            set;
        }

        private SimpleContainer Container = null;
        private IEventAggregator EventAggregator = null;

        public ShellViewModel(SimpleContainer container, IEventAggregator eag) {
            this.Container = container;
            this.EventAggregator = eag;
            this.EventAggregator.Subscribe(this);

            //var connectVM = new ConnectViewModel();
            //connectVM.PropertyChanged += ConnectVM_PropertyChanged;
            //this.ShowDialog(connectVM, 400, 170);
        }

        //void ConnectVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
        //    if (e.PropertyName.Equals("IsConnected")) {
        //        this.IsShowDialog = false;
        //        this.NotifyOfPropertyChange(() => this.IsShowDialog);

        //        this.Show(new ListTriggersViewModel());
        //    }
        //}

        public async void ShowTriggers() {
            var vm = this.Container.GetInstance<TriggerListViewModel>();
            await this.Show(vm);
        }

        public async void ShowAllJobs() {
            var vm = this.Container.GetInstance<JobListViewModel>();
            await this.Show(vm);
        }

        //public void ShowListeners() {
        //    this.Show(new ListListenerViewModel());
        //}

        public async void ShowMetadata() {
            await this.ShowDialog(new MetadataViewModel(), 700, 500);
        }

        //public void ShowAddJob(EditJobViewModel vm = null) {
        //    this.ShowDialog(vm ?? new EditJobViewModel(), 900, 500);
        //}

        //public void ShowAllCalendars(ListCalendarsViewModel vm = null) {
        //    this.ShowDialog(vm ?? new ListCalendarsViewModel(), 700, 500);
        //}

        //public void ShowAddCalendar(EditCalendarViewModel vm = null) {
        //    this.ShowDialog(vm ?? new EditCalendarViewModel(), 700, 500);
        //}

        public async Task Show(BaseScreen screen) {
            this.CurrentVM = screen;
            this.DisplayName = string.Format("任务调度管理 - {0}", this.CurrentVM.Title);
            await this.CurrentVM.Update();
            this.NotifyOfPropertyChange(() => this.CurrentVM);
        }

        public async Task ShowDialog(BaseScreen screen, int width, int height) {
            this.DialogVM = screen;
            await this.DialogVM.Update();

            this.IsShowDialog = true;
            this.DialogTitle = screen.DisplayName;
            this.DialogWidth = width;
            this.DialogHeight = height;

            this.NotifyOfPropertyChange(() => this.DialogVM);
            this.NotifyOfPropertyChange(() => this.IsShowDialog);
            this.NotifyOfPropertyChange(() => this.DialogTitle);
            this.NotifyOfPropertyChange(() => this.DialogWidth);
            this.NotifyOfPropertyChange(() => this.DialogHeight);
        }

        public void HideDialog() {
            this.IsShowDialog = false;
            this.NotifyOfPropertyChange(() => this.IsShowDialog);
        }

        public async void Handle(OpenRequest message) {
            if (message.OpenAsDialog)
                await this.ShowDialog(message.VM, message.Width ?? 800, message.Height ?? 500);
            else
                await this.Show(message.VM);
        }
    }
}
