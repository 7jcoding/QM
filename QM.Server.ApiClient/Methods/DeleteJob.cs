using QM.Server.ApiClient.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.ApiClient.Methods {
    public class DeleteJob : BaseMethod<bool> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Delete;
            }
        }

        public override string Model {
            get {
                return "Jobs";
            }
        }

        [Required, Param]
        public string Name { get; set; }

        [Param]
        public string Group { get; set; }
    }
}
