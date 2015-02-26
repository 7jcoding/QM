using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Common {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ParameterTypeAttribute : Attribute {

        public Type ParameterType {
            get;
            private set;
        }

        public ParameterTypeAttribute(Type type) {
            if (type == null)
                throw new ArgumentNullException("type");

            this.ParameterType = type;
        }

    }
}
