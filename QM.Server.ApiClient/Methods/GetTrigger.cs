using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using QM.Server.ApiClient.Attributes;

namespace QM.Server.ApiClient.Methods {
    public class GetTrigger : BaseMethod<TriggerInfo> {

        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Model {
            get {
                return "Triggers";
            }
        }

        [Param]
        public string Group { get; set; }

        [Param, Required]
        public string Name { get; set; }
    }
}
