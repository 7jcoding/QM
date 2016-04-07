using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http.Formatting;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web.ModelBinding;

namespace QM.Server.WebApi.Attributes {
    public class SigAttribute : ActionFilterAttribute {

        public async override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken) {
            //var content = actionContext.Request.Content;
            //var query = actionContext.Request.GetQueryNameValuePairs();
            //if (content.IsFormData()) {
            //    var form = await content.ReadAsFormDataAsync();
            //}

        }
    }
}
