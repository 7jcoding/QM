using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.WebApi.Auth {
    class AppUserManager : UserManager<AppUser> {

        public AppUserManager(IUserStore<AppUser> store)
            : base(store) {
        }

        protected override Task<bool> VerifyPasswordAsync(IUserPasswordStore<AppUser, string> store, AppUser user, string password) {
            return base.VerifyPasswordAsync(store, user, password);
        }

        internal static AppUserManager Create() {
            return new AppUserManager(new UserStore());
        }
    }
}
