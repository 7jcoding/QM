using System;
using System.Net;

namespace QM.Server.ApiClient {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public class MethodExecuteException : Exception {

        public HttpStatusCode StateCode {
            get; set;
        }

        public ErrorTypes ErrorType { get; set; }

        public Uri Uri { get; set; }
    }
}
