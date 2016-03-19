using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
