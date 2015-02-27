using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace QM.RemoteLoader {
    public class RemoteObject : MarshalByRefObject, IDisposable, ISponsor {


        public Assembly CurrAssembly {
            get;
            private set;
        }

        private object Instance {
            get;
            set;
        }

        private Type Type {
            get;
            set;
        }

        public void Init(string assemblyFile, string typeName) {
            this.CurrAssembly = Assembly.LoadFrom(assemblyFile);
            this.Type = this.CurrAssembly.GetType(typeName);
            if (this.Type != null)
                this.Instance = Activator.CreateInstance(this.Type);
        }

        public T ExecuteMethod<T>(string methodName, params object[] parameters) {
            return (T)this.Type.GetMethod(methodName).Invoke(this.Instance, parameters);
        }

        public void ExecuteMethod(string methodName, params object[] parameters) {
            this.Type.GetMethod(methodName).Invoke(this.Instance, parameters);
        }


        //public override object InitializeLifetimeService() {
        //    ILease lease = (ILease)base.InitializeLifetimeService();
        //    if (lease.CurrentState == LeaseState.Initial) {
        //        lease.InitialLeaseTime = TimeSpan.FromMinutes(1);
        //        lease.SponsorshipTimeout = TimeSpan.FromMinutes(2);
        //        lease.RenewOnCallTime = TimeSpan.FromSeconds(2);
        //    }
        //    return lease;
        //}

        ~RemoteObject() {
            Dispose(false);
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing && this.Instance != null) {
                var method = this.Type.GetMethod("Dispose");
                if (method != null)
                    method.Invoke(this.Instance, null);
            }
        }

        /// <summary>
        /// 远程对象续约
        /// </summary>
        public TimeSpan Renewal(ILease lease) {
#if DEBUG
            Console.WriteLine("续约");
#endif
            return TimeSpan.FromMinutes(5);
        }
    }
}
