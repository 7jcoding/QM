using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Server.WebApi.Auth {
    class UserStore : IUserStore<AppUser> {
        public Task CreateAsync(AppUser user) {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AppUser user) {
            throw new NotImplementedException();
        }

        public void Dispose() {

        }

        public Task<AppUser> FindByIdAsync(string userId) {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByNameAsync(string userName) {
            var section = ConfigurationHelper.GetSection<UserConfig>("User");
            if (section.User.Equals(userName, StringComparison.OrdinalIgnoreCase)) {
                var user = new AppUser() {
                    UserName = section.User
                };
                return Task.FromResult(user);
            } else
                return Task.FromResult<AppUser>(null);
        }


        public Task UpdateAsync(AppUser user) {
            throw new NotImplementedException();
        }
    }
}
