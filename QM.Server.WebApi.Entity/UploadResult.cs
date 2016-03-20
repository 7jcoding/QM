using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.WebApi.Entity {
    public enum UploadStates {
        Success,
        NotMutiPart,
        NoFileFoundFromRequest,
        NotZipFile,
        InvalidName,
        NameExists
    }
}
