using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QM.Server.WebApi.Auth {
    class AppUser : IUser<string> {
        public string Id {
            get {
                return this.UserName;
            }
        }

        public string UserName {
            get; set;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager manager, string authenticationType) {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }
}
