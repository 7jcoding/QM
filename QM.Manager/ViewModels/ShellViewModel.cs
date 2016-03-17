using Caliburn.Micro;
using QM.Manager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Manager.ViewModels {
    public class ShellViewModel : Screen, IShell {

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

        public ShellViewModel() {
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

        public void ShowTriggers() {
            this.Show(new TriggerListViewModel());
        }

        //public void ShowAllJobs() {
        //    this.Show(new ListJobsViewModel());
        //}

        //public void ShowRunningJobs() {

        //}

        //public void ShowListeners() {
        //    this.Show(new ListListenerViewModel());
        //}

        public void ShowMetadata() {
            this.ShowDialog(new MetadataViewModel(), 700, 500);
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

        public void Show(BaseScreen screen) {
            this.CurrentVM = screen;
            this.DisplayName = string.Format("任务调度管理 - {0}", this.CurrentVM.Title);
            this.CurrentVM.Update();
            this.NotifyOfPropertyChange(() => this.CurrentVM);
        }

        public void ShowDialog(BaseScreen screen, int width, int height) {
            this.DialogVM = screen;
            this.DialogVM.Update();

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
    }
}
