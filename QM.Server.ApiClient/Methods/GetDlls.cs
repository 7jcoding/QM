using Newtonsoft.Json;
using QM.Server.ApiClient.Attributes;
using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.ApiClient.Methods {
    public class GetDlls : BaseMethod<IEnumerable<string>> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Model {
            get {
                return "Discover/List";
            }
        }

        [Param]
        public string RelativePath { get; set; }

        [Param]
        public bool Traversal { get; private set; }

        [Param]
        public bool ShowFolder { get; private set; }

        public GetDlls() {
            this.Traversal = true;
            this.ShowFolder = false;
        }

        protected override IEnumerable<string> Parse(byte[] result) {
            var json = Encoding.UTF8.GetString(result);
            return JsonConvert.DeserializeObject<IEnumerable<DiscoverResult>>(json)
                .Select(d => d.RelativePath)
                .OrderBy(d => d, StringComparer.OrdinalIgnoreCase);
        }
    }
}
