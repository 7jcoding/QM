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
    }

    public abstract class BaseMethod<T> : BaseMethod {

    }
}
