using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Manager.Common {
    public enum InstanceMode {

        /// <summary>
        /// 不注册
        /// </summary>
        None = 0,

        /// <summary>
        /// 单例
        /// </summary>
        Singleton,

        /// <summary>
        /// 每次实例
        /// </summary>
        PreRequest
    }


    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RegistAttribute : Attribute {

        public InstanceMode Mode { get; private set; }

        public Type ForType { get; set; }

        public RegistAttribute(InstanceMode mode) {
            this.Mode = mode;
        }

    }
}
