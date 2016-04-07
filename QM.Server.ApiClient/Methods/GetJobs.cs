using QM.Server.WebApi.Entity;
using System.Collections.Generic;
using System.Net.Http;

namespace QM.Server.ApiClient.Methods {
    public class GetJobs : BaseMethod<IEnumerable<JobInfo>> {
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
    }
}
