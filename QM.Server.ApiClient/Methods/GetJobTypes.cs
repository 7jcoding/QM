using QM.Server.ApiClient.Attributes;
using QM.Server.WebApi.Entity;
using System.Collections.Generic;
using System.Net.Http;

namespace QM.Server.ApiClient.Methods {
    public class GetJobTypes : BaseMethod<IEnumerable<TypeInfo>> {
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
