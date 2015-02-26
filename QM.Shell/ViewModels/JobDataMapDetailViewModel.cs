using Caliburn.Micro;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.ViewModels {
    /// <summary>
    /// 任务参数界面
    /// </summary>
    public class JobDataMapDetailViewModel : PropertyChangedBase {

        private JobDataMap DataMap;

        public bool IsReadonly {
            get;
            set;
        }

        /// <summary>
        /// 参数列表
        /// <remarks>key 参数名, value 参数值</remarks>
        /// </summary>
        public BindableCollection<KeyValuePair<string, object>> KVS {
            get;
            set;
        }

        public JobDataMapDetailViewModel() {
            this.KVS = new BindableCollection<KeyValuePair<string, object>>();
        }

        public void Update(JobDataMap map, bool isReadonly = true) {
            this.DataMap = map;
            this.KVS = new BindableCollection<KeyValuePair<string, object>>(map.Cast<KeyValuePair<string, object>>());
            this.IsReadonly = isReadonly;

            this.NotifyOfPropertyChange(() => this.KVS);
            this.NotifyOfPropertyChange(() => this.IsReadonly);
        }

    }
}
