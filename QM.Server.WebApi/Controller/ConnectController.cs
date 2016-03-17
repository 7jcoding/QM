using QM.Server.WebApi.Models;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
