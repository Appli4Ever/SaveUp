using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaveUp.Models.Enum;
using SaveUp.Models.ViewModels;
using SaveUp.Web.API.Entities;

namespace SaveUp.Web.API.Services
{
    public class UserService : IUserService
    {
        private readonly SaveUpDbContext context;
        private readonly ITokenService tokenService;
        private readonly IUser user;

        public UserService(SaveUpDbContext context, ITokenService tokenService, IUser user)
        {
            this.context = context;
            this.tokenService = tokenService;
            this.user = user;
        }

        public async Task<UserViewModel?> CreateUser(UserViewModel model)
        {
            try
            {
                var user = await this.context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

                if (user is not null)
                {
                    return null;
                }

                var hasher = new PasswordHasher<User>();

                user = new User()
                {
                    Username = model.Username,
                    LoginBlocked = false,
                    LoginTries = 0,
                };

                user.Password = hasher.HashPassword(user, model.Password);

                await this.context.Users.AddAsync(user);
                await this.context.SaveChangesAsync();

                return new UserViewModel
                {
                    Username = user.Username,
                };

            }
            catch
            {
                return null;
            }
        }

        public async Task<TokenViewModel> CheckLogin(UserViewModel model)
        {
            try
            {
                var user = await this.context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

                if (user == null)
                {
                    return new TokenViewModel()
                    {
                        LoginStatus = LoginStatus.Faild,
                    };
                }

                if (user.LoginBlocked)
                {
                    return new TokenViewModel()
                    {
                        LoginStatus = LoginStatus.Blocked,
                    };
                }

                var hasher = new PasswordHasher<User>();

                if (hasher.VerifyHashedPassword(user, user.Password, model.Password) == PasswordVerificationResult.Success)
                {
                    user.LoginTries = 0;
                    await this.context.SaveChangesAsync();

                    return new TokenViewModel()
                    {
                        Token = this.tokenService.CreateToken(user.Id, user.Username),
                        LoginStatus = LoginStatus.Success,
                    };
                }

                user.LoginTries++;
                if (user.LoginTries > 2)
                {
                    user.LoginBlocked = true;
                }

                await this.context.SaveChangesAsync();

                return new TokenViewModel()
                {
                    LoginStatus = user.LoginBlocked ? LoginStatus.Blocked : LoginStatus.Faild,
                };
            }
            catch
            {
                return new TokenViewModel() { LoginStatus = LoginStatus.Faild };
            }
        }

        public async Task<bool> ChangePassword(PasswordViewModel model)
        {
            try
            {
                var user = await this.context.Users.FindAsync(this.user.Id);

                if (user is null)
                {
                    return false;
                }

                if (model.Password != model.VerifiyPassword)
                {
                    return false;
                }

                var hasher = new PasswordHasher<User>();

                user.Password = hasher.HashPassword(user, model.Password);

                var rows = await this.context.SaveChangesAsync();

                return rows > 0;
            }
            catch
            {
                return false;
            }

        }
    }
}
