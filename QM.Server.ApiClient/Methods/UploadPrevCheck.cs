using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using QM.Server.ApiClient.Attributes;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace QM.Server.ApiClient.Methods {
    public class UploadPrevCheck : BaseMethod<UploadStates> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Model {
            get {
                return "Upload/CheckName";
            }
        }

        [Param, Required]
        public string Name { get; private set; }

        public UploadPrevCheck(string name, string file = null) {
            this.Name = name;
            if (string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(file)) {
                this.Name = Path.GetFileName(file);
            }
        }
    }
}
