namespace QM.Shell {
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using QM.Shell.ViewModels;
    using QM.Shell.Interfaces;
    using QM.Shell.Common;

    public class AppBootstrapper : BootstrapperBase {
        SimpleContainer container;

        public AppBootstrapper() {
            this.StartRuntime();
        }

        protected override void Configure() {
            container = new SimpleContainer();
            GlobalData.SetContainer(container);

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.Singleton<IShell, ShellViewModel>();//这里必须是，Singleton 因为 ShellViewModel 要在其它子界面中用到,
        }

        protected override object GetInstance(Type service, string key) {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<IShell>();
        }

        protected override void OnExit(object sender, EventArgs e) {
            base.OnExit(sender, e);
        }
    }
}