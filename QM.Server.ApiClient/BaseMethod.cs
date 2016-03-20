using Microsoft.Practices.EnterpriseLibrary.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public virtual HttpContent GetContent() {
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

        protected async virtual Task<byte[]> GetResult(ApiClient client) {
            using (var content = this.GetContent())
            using (var hc = new HttpClient()) {
                var request = new HttpRequestMessage(this.HttpMethod, client.BuildUri(this, content));
                if (content != null)
                    request.Content = content;
                var rep = await hc.SendAsync(request);
                return await rep.Content.ReadAsByteArrayAsync();
            }
        }
    }

    public abstract class BaseMethod<T> : BaseMethod {

        protected virtual T Parse(byte[] result) {
            var json = Encoding.UTF8.GetString(result);
            return JsonConvert.DeserializeObject<T>(json);
        }


        internal async Task<T> Execute(ApiClient client) {
            var bytes = await this.GetResult(client);
            return this.Parse(bytes);
        }
    }
}
