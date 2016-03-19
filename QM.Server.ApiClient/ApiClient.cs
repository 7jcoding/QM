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

        private string BuildUri(BaseMethod mth, HttpContent content = null) {
            var url = string.Format("http://localhost:5556/api/{0}", mth.Model);
            if (content == null) {
                return url.SetUrlKeyValue(mth.GetParams());
            } else
                return url;
        }

        public async Task<T> Execute<T>(BaseMethod<T> method) {
            if (method == null)
                throw new ArgumentNullException("method");

            var results = method.Validate();
            if (!results.IsValid) {
                throw new MethodValidationException(results);
            }

            using (var content = method.GetContent())
            using (var client = new HttpClient()) {
                var request = new HttpRequestMessage(method.HttpMethod, this.BuildUri(method, content));
                if (content != null)
                    request.Content = content;
                var rep = await client.SendAsync(request);
                var json = await rep.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}
