using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.ApiClient.Methods {
    public class Upload : BaseMethod<UploadStates> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Post;
            }
        }

        public override string Model {
            get {
                return "Upload";
            }
        }

        public string FilePath { get; set; }

        public string Name { get; set; }

        protected override IEnumerable<string> InnerValidate() {
            if (!File.Exists(this.FilePath)) {
                yield return "要上传的文件不存在";
            }
        }

        public override HttpContent GetContent() {
            var content = new MultipartFormDataContent();
            var fs = new FileStream(this.FilePath, FileMode.Open, FileAccess.Read);
            var fileContent = new ByteArrayContent(File.ReadAllBytes(this.FilePath));
            content.Add(fileContent, "file", Path.GetFileName(this.FilePath));

            var nameContent = new StringContent(this.Name ?? "");
            content.Add(nameContent, "name");

            var str = content.ReadAsStringAsync().Result;

            return content;
        }
    }
}
