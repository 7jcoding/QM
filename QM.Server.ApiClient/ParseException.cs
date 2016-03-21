using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.ApiClient {
    public class ParseException : Exception {

        public byte[] TargetData { get; set; }

        public Type TargetType { get; set; }
    }
}
