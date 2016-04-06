using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
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

        public event EventHandler<ApiClientMessageArgs> OnMessage = null;

        private ApiClient() {

        }

        public string BuildUri(BaseMethod mth, HttpContent content = null) {
            var url = string.Format("http://localhost:5556/api/{0}", mth.Model);
            if (content == null) {
                return url.SetUrlKeyValue(mth.GetParams().ToDictionary(d => d.Key, d => d.Value.ToString()));
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

            try {
                return await method.Execute(this);
            } catch (HttpRequestException ex) {
                if (this.OnMessage != null)
                    this.OnMessage.Invoke(method, new ApiClientMessageArgs() {
                        ErrorType = ErrorTypes.Unknow,
                        Message = ex.Message
                    });
                return default(T);
            } catch (ParseException) {
                if (this.OnMessage != null)
                    this.OnMessage.Invoke(method, new ApiClientMessageArgs() {
                        ErrorType = ErrorTypes.ParseError,
                    });

                return default(T);
            } catch (MethodExecuteException ex3) {
                if (this.OnMessage != null)
                    this.OnMessage.Invoke(method, new ApiClientMessageArgs() {
                        ErrorType = ex3.ErrorType
                    });

                return default(T);
            }
        }
    }

    public sealed class ApiClientMessageArgs : EventArgs {
        public ErrorTypes? ErrorType {
            get; set;
        }

        public string Message { get; set; }
    }
}
