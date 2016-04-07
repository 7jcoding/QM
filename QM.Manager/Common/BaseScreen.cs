using Caliburn.Micro;
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

        public abstract Task Update();

        public override string DisplayName {
            get {
                return this.Title;
            }
        }
    }
}
