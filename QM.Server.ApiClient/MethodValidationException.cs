using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;

namespace QM.Server.ApiClient {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public class MethodValidationException : Exception {
        public ValidationResults ValidationResult {
            get; private set;
        }

        public MethodValidationException(ValidationResults results) {
            this.ValidationResult = results;
        }
    }
}
