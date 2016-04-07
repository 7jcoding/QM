using System;

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
