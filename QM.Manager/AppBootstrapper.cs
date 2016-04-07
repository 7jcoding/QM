namespace QM.Manager {
    using Caliburn.Micro;
    using Common;
    using Server.ApiClient;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using ViewModels;
    public class AppBootstrapper : BootstrapperBase {
        SimpleContainer container;

        public AppBootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();

            container.RegisterInstance(typeof(SimpleContainer), null, container);
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();

            container.PerRequest<IShell, ShellViewModel>();

            this.RegistInstances(container);
        }

        private void RegistInstances(SimpleContainer _container) {
            var types = this.GetType().GetTypeInfo().Assembly.DefinedTypes
                .Select(t => {
                    var attr = t.GetCustomAttribute<RegistAttribute>();
                    return new {
                        T = t,
                        Mode = attr?.Mode,
                        TargetType = attr?.ForType
                    };
                })
                .Where(o => o.Mode != null && o.Mode != InstanceMode.None);

            foreach (var t in types) {
                var type = t.T.AsType();
                if (t.Mode == InstanceMode.Singleton) {
                    _container.RegisterSingleton(t.TargetType ?? type, null, type);
                } else if (t.Mode == InstanceMode.PreRequest) {
                    _container.RegisterPerRequest(t.TargetType ?? type, null, type);
                }
            }
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

            ApiClient.Instance.OnMessage += Instance_OnMessage;
        }

        private void Instance_OnMessage(object sender, ApiClientMessageArgs e) {
            MessageBox.Show(e.Message ?? e.ErrorType?.ToString());
        }
    }
}