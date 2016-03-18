using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.ApiClient {
    public class MethodValidationException : Exception {
        public ValidationResults ValidationResult {
            get; private set;
        }

        public MethodValidationException(ValidationResults results) {
            this.ValidationResult = results;
        }
    }
}
