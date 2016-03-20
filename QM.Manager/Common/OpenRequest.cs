using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Manager.Common {
    public class OpenRequest {

        public BaseScreen VM { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public bool OpenAsDialog { get; set; }
    }
}
