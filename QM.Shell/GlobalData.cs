using Caliburn.Micro;
using QM.Shell.Common;
using QM.Shell.Interfaces;
using QM.Shell.ViewModels;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell {
    public static class GlobalData {

        private static SimpleContainer Container;

        public static Lazy<IEnumerable<Type>> CalendarEditors = new Lazy<IEnumerable<Type>>(() => {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsPublic
                    && !t.IsAbstract
                    && typeof(CalendarEditor).IsAssignableFrom(t));
        });

        public static Lazy<IEnumerable<Type>> ScheduleBuildBys = new Lazy<IEnumerable<Type>>(() => {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsPublic
                && !t.IsAbstract
                && typeof(IScheduleBuildByVM).IsAssignableFrom(t));

        });

        public static void SetContainer(SimpleContainer container) {
            Container = container;
        }

        public static ShellViewModel GetShell() {
            return (ShellViewModel)Container.GetInstance<IShell>();
        }
    }
}
