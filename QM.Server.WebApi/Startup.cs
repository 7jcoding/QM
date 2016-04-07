using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using QM.Server.WebApi.Auth;
using System;
using System.Web.Http;


namespace QM.Server.WebApi {
    public class Startup {

        public void Configuration(IAppBuilder app) {

            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            // 配置 Json.net，使其支持从抽象类型转换到具体的类型
            config.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings() {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto
            };

            //配置授权认证
            app.CreatePerOwinContext(AppUserManager.Create);
            var oauthOptions = new OAuthAuthorizationServerOptions {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new AppOAuthProvider("_SELF"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                //在生产模式下设 AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // 使应用程序可以使用不记名令牌来验证用户身份
            app.UseOAuthBearerTokens(oauthOptions);

            config.EnableCors();

            app.UseWebApi(config);
            config.EnsureInitialized();
        }

    }
}
