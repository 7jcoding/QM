using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Manager.Common {
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseScreen : Screen {

        /// <summary>
        /// 窗体标题
        /// </summary>
        public abstract string Title {
            get;
        }

        public abstract void Update();

        public override string DisplayName {
            get {
                return this.Title;
            }
        }
    }
}
