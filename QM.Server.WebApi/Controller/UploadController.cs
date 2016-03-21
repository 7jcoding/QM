using QM.Server.WebApi.Entity;
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
    public class UploadController : ApiController {

        private static readonly string TmpDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp");

        [HttpPost]
        public async Task<UploadStates> Post(HttpRequestMessage request) {
            IEnumerable<MultipartFileData> files = null;
            try {
                if (!Request.Content.IsMimeMultipartContent()) {
                    return UploadStates.NotMutiPart;
                }
                
                if (!Directory.Exists(TmpDir))
                    Directory.CreateDirectory(TmpDir);

                var p = new MultipartFormDataStreamProvider(TmpDir);

                //Must
                await request.Content.ReadAsMultipartAsync(p);

                var name = p.FormData.Get("name");

                if (p.FileData.Count == 0)
                    return UploadStates.NoFileFoundFromRequest;

                files = p.FileData;
                var file = p.FileData.First();
                if (string.IsNullOrWhiteSpace(name)) {
                    name = Path.GetFileNameWithoutExtension(file.Headers.ContentDisposition.FileName);
                }

                //var targetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jobs", name);
                //if (Directory.Exists(targetDir)) {
                //    return UploadStates.NameExists;
                //}
                var t = this.CheckName(name);
                if (t != UploadStates.Success)
                    return t;

                var targetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jobs", name);
                try {
                    ZipFile.ExtractToDirectory(file.LocalFileName, targetDir);
                } catch {
                    return UploadStates.NotZipFile;
                }

                return UploadStates.Success;
            } finally {
                if (files != null) {
                    foreach (var f in files) {
                        File.Delete(f.LocalFileName);
                    }
                }
            }
        }

        [HttpGet]
        public UploadStates CheckName(string name) {
            if (string.IsNullOrWhiteSpace(name))
                return UploadStates.InvalidName;

            if (name.Intersect(Path.GetInvalidPathChars()).Any()) {
                return UploadStates.InvalidName;
            }

            var targetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jobs", name);
            if (Directory.Exists(targetDir)) {
                return UploadStates.NameExists;
            }

            return UploadStates.Success;
        }
    }
}
