using Microsoft.Practices.EnterpriseLibrary.Validation;
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
    }

    public abstract class BaseMethod<T> : BaseMethod {

    }
}
