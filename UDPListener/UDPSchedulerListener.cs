using Quartz;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPListener {
    public class UDPSchedulerListener : ISchedulerListener, IDisposable {


        private UdpClient Client;

        public UDPSchedulerListener() {
            this.Client = new UdpClient();
            this.Client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
        }

        public void JobAdded(IJobDetail jobDetail) {
            var msg = string.Format("添加新任务 {0} / {1}\r\n{2}", jobDetail.Key.Name, jobDetail.Key.Group, jobDetail.Description);
            this.Send(msg);
        }

        public void JobDeleted(JobKey jobKey) {
            var msg = string.Format("删除任务 {0} /{1}", jobKey.Name, jobKey.Group);
            this.Send(msg);
        }

        public void JobPaused(JobKey jobKey) {
            var msg = string.Format("挂起任务 {0} /{1}", jobKey.Name, jobKey.Group);
            this.Send(msg);
        }

        public void JobResumed(JobKey jobKey) {
            var msg = string.Format("恢复任务 {0} /{1}", jobKey.Name, jobKey.Group);
            this.Send(msg);
        }

        public void JobScheduled(ITrigger trigger) {
            //throw new NotImplementedException();
        }

        public void JobUnscheduled(TriggerKey triggerKey) {
            //throw new NotImplementedException();
        }

        public void JobsPaused(string jobGroup) {
            //throw new NotImplementedException();
        }

        public void JobsResumed(string jobGroup) {
            //throw new NotImplementedException();
        }

        public void SchedulerError(string msg, SchedulerException cause) {
            //throw new NotImplementedException();
            this.Send(string.Format("异常 : {0}\r\n{1}", msg, cause.GetBaseException().StackTrace));
        }

        public void SchedulerInStandbyMode() {
            //throw new NotImplementedException();
        }

        public void SchedulerShutdown() {
            this.Send("调度服务已关闭");
        }

        public void SchedulerShuttingdown() {
            this.Send("调度服务关闭中。。。");
        }

        public void SchedulerStarted() {
            this.Send("调度服务启动完成");
        }

        public void SchedulerStarting() {
            //throw new NotImplementedException();
            this.Send("调度服务启动中。。。");
        }

        public void SchedulingDataCleared() {
            this.Send("调度任务数据被清理。");
        }

        public void TriggerFinalized(ITrigger trigger) {
            var msg = string.Join("触发器所有执行计划均已完成 {0} / {1} ，应用日历：{2}", trigger.Key.Name, trigger.Key.Group, trigger.CalendarName);
        }

        public void TriggerPaused(TriggerKey triggerKey) {
            var msg = string.Format("触发器挂起 {0} /{1}", triggerKey.Name, triggerKey.Group);
        }

        public void TriggerResumed(TriggerKey triggerKey) {
            var msg = string.Format("触发器恢复 {0} /{1}", triggerKey.Name, triggerKey.Group);
        }

        public void TriggersPaused(string triggerGroup) {
            var msg = string.Format("触发器挂起 {0}", string.Join(",", triggerGroup));
        }

        public void TriggersResumed(string triggerGroup) {
            var msg = string.Format("触发器恢复 {0}", string.Join(",", triggerGroup));
        }




        private void Send(string msg) {
            var bytes = Encoding.UTF8.GetBytes(msg);
            this.Client.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Broadcast, 6666));
        }

        ~UDPSchedulerListener() {
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
