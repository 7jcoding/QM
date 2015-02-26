using Caliburn.Micro;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.ViewModels {
    public class JobDetailViewModel : PropertyChangedBase {

        private IJobDetail Job;

        public void Update(IJobDetail job) {
            this.Job = job;
            this.NotifyOfPropertyChange(() => this.Job);
        }
    }
}
