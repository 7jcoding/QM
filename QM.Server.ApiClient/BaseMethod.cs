using Microsoft.Practices.EnterpriseLibrary.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.ApiClient {
    public abstract class BaseMethod {

        public abstract string Model {
            get;
        }

        public abstract HttpMethod HttpMethod {
            get;
        }

        protected virtual object GetSendData() {
            //return this.GetParams();
            return null;
        }

        public virtual HttpContent GetContent() {
            var data = this.GetSendData();
            if (data != null) {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                return content;
            }
            return null;
        }

        /// <summary>
        /// 检查输入参数的合法性
        /// </summary>
        /// <returns></returns>
        public ValidationResults Validate() {
            var validator = ValidationFactory.CreateValidator(this.GetType());
            var results = validator.Validate(this);

            var msgs = this.InnerValidate();
            results.AddAllResults(msgs.Select(m => new ValidationResult(m, this, "", "", null)));

            return results;
        }

        /// <summary>
        /// 验证数据（虚方法，在子类中重写验证）
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<string> InnerValidate() {
            return Enumerable.Empty<string>();
        }

        protected async virtual Task<Tuple<HttpStatusCode, byte[]>> GetResult(ApiClient client) {
            using (var content = this.GetContent())
            using (var hc = new HttpClient()) {
                var request = new HttpRequestMessage(this.HttpMethod, client.BuildUri(this, content));
                if (content != null) {
                    request.Content = content;
                }

                var rep = await hc.SendAsync(request);
                var bytes = await rep.Content.ReadAsByteArrayAsync();
                return new Tuple<HttpStatusCode, byte[]>(rep.StatusCode, bytes);
            }
        }
    }

    public abstract class BaseMethod<T> : BaseMethod {

        protected virtual T Parse(byte[] result) {
            var json = Encoding.UTF8.GetString(result);
            return JsonConvert.DeserializeObject<T>(json);
        }


        internal async Task<T> Execute(ApiClient client) {
            var result = await this.GetResult(client);
            var status = result.Item1;

            var error = status.Convert();
            if (error.HasValue) {
                throw new MethodExecuteException() {
                    StateCode = status,
                    ErrorType = error.Value
                };
            }

            try {
                return this.Parse(result.Item2);
            } catch {
                throw new ParseException() {
                    TargetType = typeof(T),
                    TargetData = result.Item2
                };
            }
        }
    }
}
