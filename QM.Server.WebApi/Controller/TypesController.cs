using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QM.Server.WebApi.Controller {

    [EnableCors("*", "*", "*")]
    public class TypesController : ApiController {

        private static readonly string TmpDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp");

        [HttpPost]
        public async void Post(HttpRequestMessage request) {
            if (!Request.Content.IsMimeMultipartContent()) {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            if (!Directory.Exists(TmpDir))
                Directory.CreateDirectory(TmpDir);

            var p = new MultipartFormDataStreamProvider(TmpDir);

            //Must
            await request.Content.ReadAsMultipartAsync(p);

            var name = p.FormData.Get("name");

            if (p.FileData.Count == 0)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) {
                    ReasonPhrase = "Not found file in request"
                });

            var file = p.FileData.First();
            if (string.IsNullOrWhiteSpace(name)) {
                name = Path.GetFileNameWithoutExtension(file.Headers.ContentDisposition.FileName);
            }

            this.Save(file, name);
        }

        private void Save(MultipartFileData file, string name) {
            if (!file.Headers.ContentType.MediaType.Equals("application/x-zip-compressed")) {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) {
                    ReasonPhrase = "Only Support Zip file"
                });
            }

            var targetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jobs", name);
            if (Directory.Exists(targetDir)) {
                throw new Exception(targetDir + "已经存在");
            }

            ZipFile.ExtractToDirectory(file.LocalFileName, targetDir);
        }
    }
}
