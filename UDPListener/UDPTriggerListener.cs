using Quartz;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPListener {
    public class UDPTriggerListener : ITriggerListener, IDisposable {

        private UdpClient Client;
        public string Name {
            get {
                return "UDP 广播";
            }
        }

        public UDPTriggerListener() {
            this.Client = new UdpClient();
            this.Client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
        }

        public void TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode) {
            var msg = string.Format("触发器 {0} / {1} , 任务 {2} / {3} 执行完毕", trigger.Key.Name, trigger.Key.Name, trigger.JobKey.Name, trigger.JobKey.Group);
            this.Send(msg);
        }

        public void TriggerFired(ITrigger trigger, IJobExecutionContext context) {
            var msg = string.Format("触发器 {0} / {1} , 任务 {2} / {3} 开始执行", trigger.Key.Name, trigger.Key.Name, trigger.JobKey.Name, trigger.JobKey.Group);
            this.Send(msg);
        }

        public void TriggerMisfired(ITrigger trigger) {
            var msg = string.Format("触发器 {0} / {1} , 任务 {2} / {3} 未在规定的时间执行", trigger.Key.Name, trigger.Key.Name, trigger.JobKey.Name, trigger.JobKey.Group);
            this.Send(msg);
        }

        public bool VetoJobExecution(ITrigger trigger, IJobExecutionContext context) {
            return false;
        }

        private void Send(string msg) {
            var bytes = Encoding.UTF8.GetBytes(msg);
            this.Client.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Broadcast, 6666));
        }

        ~UDPTriggerListener() {
            Dispose(false);
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (this.Client != null) {
                    this.Client.Client.Dispose();
                    this.Client.Close();
                }
            }
        }





    }
}
