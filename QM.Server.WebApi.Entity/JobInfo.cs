using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.WebApi.Entity {
    public class JobInfo {

        public string Name { get; set; }

        public string Group { get; set; }

        public string Desc { get; set; }

        public string JobType { get; set; }

        public Dictionary<string, object> DataMap { get; set; }

        public bool Durability { get; set; }

        public bool ShouldRecover { get; set; }
    }
}
