using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace QM.RemoteLoader {
    //[Serializable]
    public class IsolateDomainLoader : IDisposable {
        private AppDomain Domain;

        private RemoteObjectSponsor Sponsor = new RemoteObjectSponsor();

        public IsolateDomainLoader(string path, string configFileName = "") {
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationName = "IsolateDomainLoader";
            setup.ApplicationBase = path;//AppDomain.CurrentDomain.BaseDirectory;
            setup.DynamicBase = path;
            setup.PrivateBinPath = path;
            setup.CachePath = setup.ApplicationBase;
            setup.ShadowCopyFiles = "true";
            setup.ShadowCopyDirectories = setup.ApplicationBase;
            if (!string.IsNullOrWhiteSpace(configFileName)) {
                setup.ConfigurationFile = configFileName;
                setup.ConfigurationFile = Path.Combine(path, configFileName);
            }
            this.Domain = AppDomain.CreateDomain("ApplicationLoaderDomain", null, setup);
        }


        public RemoteObject GetObject(string assemblyFile, string typeFullName) {
            String name = Assembly.GetExecutingAssembly().FullName;
            //如果用 CreateInstanceAndUnwrap 只能把 ApplicationBas 设为主程充的 BaseDirectory, 这样子域就不能有自己的 ApplicationBase 了.
            //var obj = (RemoteObject)this.Domain.CreateInstanceAndUnwrap(name, typeof(RemoteObject).FullName);
            var obj = (RemoteObject)this.Domain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().Location, typeof(RemoteObject).FullName);
            obj.Init(assemblyFile, typeFullName);

            ILease lease = (ILease)obj.GetLifetimeService();
            lease.Register(this.Sponsor);

            return obj;
        }

        public void Unload() {
            if (Domain == null)
                return;

            try {
                AppDomain.Unload(this.Domain);
                this.Domain = null;
            } catch {
            }
        }

        ~IsolateDomainLoader() {
            Dispose(false);
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                this.Unload();
#if DEBUG
                Console.WriteLine("Domain Unloaded");
#endif
            }
        }
    }
}
