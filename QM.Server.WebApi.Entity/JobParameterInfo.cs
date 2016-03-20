using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.WebApi.Entity {
    public class JobParameterInfo {

        public string Name { get; set; }

        public string Type { get; set; }

        public object Value { get; set; }

        public string Desc { get; set; }
    }
}
