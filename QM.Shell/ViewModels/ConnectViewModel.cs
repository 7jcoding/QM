using AsNum.WPF.Controls;
using Caliburn.Micro;
using QM.Shell.Common;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QM.Shell.ViewModels {
    /// <summary>
    /// 连接界面
    /// </summary>
    public class ConnectViewModel : SchedulerScreen {

        public Connection Connection { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Info { get; set; }


        public bool NeedShowInfo { get; set; }

        /// <summary>
        /// 是否已连接
        /// </summary>
        public bool IsConnected { get; set; }

        public ConnectViewModel() {
            this.Connection = new Connection();
        }
        public void Connect() {
            this.Info = "尝试连接,请稍候";
            this.NeedShowInfo = true;
            this.NotifyOfPropertyChange(() => this.NeedShowInfo);
            this.NotifyOfPropertyChange(() => this.Info);
            DispatcherHelper.DoEvents();

            if (Connection.Connect(this.Connection)) {
                this.IsConnected = true;
                this.NeedShowInfo = false;
                this.NotifyOfPropertyChange(() => this.NeedShowInfo);
                this.NotifyOfPropertyChange(() => this.IsConnected);
            } else {
                this.Info = "连接失败,请检查";
                this.NeedShowInfo = true;
                this.NotifyOfPropertyChange(() => this.Info);
                this.NotifyOfPropertyChange(() => this.NeedShowInfo);

                DispatcherHelper.DoEvents();
                Thread.Sleep(2000);

                this.NeedShowInfo = false;
                this.NotifyOfPropertyChange(() => this.NeedShowInfo);
            }
        }

        public override void Update(IScheduler scheduler) {
            
        }

        public override string Title {
            get {
                return "服务连接";
            }
        }
    }
}
