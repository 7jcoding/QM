using Quartz.Simpl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server {
    public class MyTypeLoadHelper : SimpleTypeLoadHelper {

        public override Type LoadType(string name) {
            return base.LoadType(name);
        }

    }
}
