using log4net;
using System;
using Topshelf;

namespace QM.Server {
    class Program {

        static ILog Log = LogManager.GetLogger("Server");

        static void Main(string[] args) {

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            HostFactory.Run(x => {
                x.SetDescription("XXY Scheduler Service");
                x.SetDisplayName("XXY");
                x.SetInstanceName("QM.Server");
                x.SetServiceName("QM.Service");

                x.Service(s => {
                    var server = new QMServer();
                    return server;
                });
            });
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            var ex = ((Exception)e.ExceptionObject).GetBaseException();
            Log.Error(ex);
        }
    }
}
