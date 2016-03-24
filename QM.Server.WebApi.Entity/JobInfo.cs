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

        public JobType JobType { get; set; }

        //public string AssemblyQualifiedName { get; set; }

        //public string AssemblyFullName { get; set; }

        //public string AssemblyFile { get; set; }

        //public string JobTypeFullName { get; set; }

        public Dictionary<string, object> DataMap { get; set; }

        public bool Durability { get; set; }

        public bool ShouldRecover { get; set; }

        /// <summary>
        /// 只用于创建任务
        /// </summary>
        public bool ReplaceExists { get; set; }
    }
}
