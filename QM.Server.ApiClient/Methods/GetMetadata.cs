using System.Collections.Generic;
using System.Net.Http;

namespace QM.Server.ApiClient.Methods {
    public class GetMetadata : BaseMethod<Dictionary<string, string>> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Model {
            get {
                return "Metadata";
            }
        }
    }
}
