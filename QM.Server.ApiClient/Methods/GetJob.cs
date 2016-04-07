using QM.Server.ApiClient.Attributes;
using QM.Server.WebApi.Entity;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

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
