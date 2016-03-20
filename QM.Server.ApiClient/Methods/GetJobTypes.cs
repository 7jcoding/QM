using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using QM.Server.ApiClient.Attributes;

namespace QM.Server.ApiClient.Methods {
    public class GetJobTypes : BaseMethod<IEnumerable<JobTypeInfo>> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Model {
            get {
                return "Discover/GetTypes";
            }
        }

        [Param]
        public string DllPath { get; set; }
    }
}
