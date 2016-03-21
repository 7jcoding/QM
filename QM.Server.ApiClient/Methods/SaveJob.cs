using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using QM.Server.ApiClient.Attributes;
using System.ComponentModel.DataAnnotations;

namespace QM.Server.ApiClient.Methods {
    public class SaveJob : BaseMethod<JobSaveStates> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Put;
            }
        }

        public override string Model {
            get {
                return "Jobs";
            }
        }

        [Required]
        public string Name { get; set; }

        public string Group { get; set; }

        public string Desc { get; set; }

        public bool Durability { get; set; }

        public bool ShouldRecover { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public bool ReplaceExists { get; set; }

        private string JobType = null;
        private string AssemblyFile = null;

        public SaveJob(string jobType, string assemblyFile) {
            if (string.IsNullOrWhiteSpace(jobType) || string.IsNullOrWhiteSpace(assemblyFile))
                throw new ArgumentException();

            this.JobType = jobType;
            this.AssemblyFile = assemblyFile;
        }

        protected override object GetSendData() {
            return new JobInfo() {
                AssemblyFile = this.AssemblyFile,
                JobTypeFullName = this.JobType,
                Desc = this.Desc,
                Durability = this.Durability,
                Group = this.Group,
                Name = this.Name,
                ShouldRecover = this.ShouldRecover,
                DataMap = this.Parameters,
                ReplaceExists = this.ReplaceExists
            };
        }
    }
}
