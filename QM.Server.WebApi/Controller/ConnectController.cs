using QM.Server.WebApi.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QM.Server.WebApi.Controller {
    [EnableCors("*", "*", "*")]
    public class ConnectController : ApiController {
        [HttpPost]
        public bool Post([FromBody]UserInfo info) {
            return true;
        }
    }
}
