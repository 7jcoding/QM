using QM.RemoteLoader;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server {
    /// <summary>
    /// http://stackoverflow.com/questions/8392596/how-can-i-run-quartz-net-jobs-in-a-separate-appdomain
    /// </summary>
    internal class IsolatedJob : IInterruptableJob, IDisposable {
        private readonly Type JobType;

        private IsolateDomainLoader IDL = null;
        private RemoteObject Obj = null;

        public IsolatedJob(Type jobType) {
            if (jobType == null)
                throw new ArgumentNullException("jobType");
            this.JobType = jobType;

            var path = Path.GetDirectoryName(this.JobType.Assembly.Location);
            this.IDL = new IsolateDomainLoader(path, string.Format("{0}.dll.config", this.JobType.Assembly.GetName().Name));
            this.Obj = this.IDL.GetObject(this.JobType.Assembly.Location, this.JobType.FullName);
        }

        public void Execute(IJobExecutionContext context) {
            //if (this.Obj == null) {
            //    this.Obj = this.IDL.GetObject(this.JobType.Assembly.Location, this.JobType.FullName);
            //}
            this.Obj.ExecuteMethod("Execute", context);

        }


        public void Interrupt() {
            //if (this.Instance != null && this.JobType.GetMethod("Interrupt", Type.EmptyTypes) != null)
            //    this.Loader.ExecuteMethod(this.Instance, "Interrupt");

            this.Obj.ExecuteMethod("Interrupt");
        }

        ~IsolatedJob() {
            Dispose(false);
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (this.Obj != null)
                this.Obj.Dispose();


            if (disposing) {
                if (this.IDL != null)
                    this.IDL.Dispose();
            }
        }
    }
}
