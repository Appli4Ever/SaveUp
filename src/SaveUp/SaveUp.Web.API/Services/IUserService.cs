using SaveUp.Models.ViewModels;

namespace SaveUp.Web.API.Services;

public interface IUserService
{
    public Task<UserViewModel?> CreateUser(UserViewModel model);

    public Task<TokenViewModel> CheckLogin(UserViewModel model);

    public Task<bool> ChangePassword(PasswordViewModel model);
}