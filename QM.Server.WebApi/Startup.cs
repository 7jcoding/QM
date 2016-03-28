using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace QM.Server.WebApi {
    public class Startup {

        public void Configuration(IAppBuilder appBuilder) {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //
            config.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings() {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto
            };

            config.EnableCors();
            appBuilder.UseWebApi(config);
            config.EnsureInitialized();
        }

    }
}
