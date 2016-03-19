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
    public class GetJob : BaseMethod<JobInfo> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Model {
            get {
                return "Jobs";
            }
        }

        [Param, Required]
        public string Name { get; set; }

        [Param]
        public string Group { get; set; }
    }
}
