using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace QM.Server.WebApi.Auth {
    class UserStore : IUserStore<AppUser>, IUserPasswordStore<AppUser> {

        #region IUserStore
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

        public Task<string> GetPasswordHashAsync(AppUser user) {
            var section = ConfigurationHelper.GetSection<UserConfig>("User");
            if (section.User.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)) {
                return Task.FromResult(section.Pwd);
            } else
                return Task.FromResult<string>(null);
        }

        public Task<bool> HasPasswordAsync(AppUser user) {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AppUser user) {
            throw new NotImplementedException();
        }


        #endregion

    }
}
