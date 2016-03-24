using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.WebApi.Entity {

    public class JobType {

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 所在的DLL文件
        /// </summary>
        public string AssemblyFile { get; set; }

        /// <summary>
        /// 类型上的说明 DescriptionAttribute
        /// </summary>
        public string TypeDesc { get; set; }

        public JobType(Type type) {
            this.FullName = type.FullName;
            this.AssemblyFile = type.Assembly.Location;

            var attr = type.GetCustomAttribute<DescriptionAttribute>();
            if (attr != null)
                this.TypeDesc = attr.Description;
        }

        public JobType(string fullName, string assemblyFile) {
            this.FullName = fullName;
            this.AssemblyFile = assemblyFile;
        }

        /// <summary>
        /// Serializer 
        /// </summary>
        public JobType() {

        }
    }
}
