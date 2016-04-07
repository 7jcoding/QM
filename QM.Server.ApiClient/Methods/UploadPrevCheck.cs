using QM.Server.ApiClient.Attributes;
using QM.Server.WebApi.Entity;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http;

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
