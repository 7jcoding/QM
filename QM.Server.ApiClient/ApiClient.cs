using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.ApiClient {
    public sealed class ApiClient {

        private static Lazy<ApiClient> _Instance = new Lazy<ApiClient>(() => new ApiClient());

        public static ApiClient Instance {
            get {
                return _Instance.Value;
            }
        }

        private ApiClient() {

        }

        private string BuildUri(BaseMethod mth) {
            return string.Format("http://localhost:5556/api/{0}", mth.Model);
        }

        public async Task<T> Execute<T>(BaseMethod<T> method) {
            using (var client = new HttpClient()) {
                var request = new HttpRequestMessage(method.HttpMethod, this.BuildUri(method));
                var rep = await client.SendAsync(request);
                var json = await rep.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}
