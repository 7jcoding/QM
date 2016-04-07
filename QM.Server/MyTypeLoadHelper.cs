using Quartz.Simpl;
using System;

namespace QM.Server {
    public class MyTypeLoadHelper : SimpleTypeLoadHelper {

        public override Type LoadType(string name) {
            return base.LoadType(name);
        }

    }
}
