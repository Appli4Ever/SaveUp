using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class LoginRegisterViewModel : ViewModelBase

    {
        private readonly HttpUserService userService;

        public UserViewModel User { get; set; }

        public LoginRegisterViewModel(HttpUserService userService)
        {
            this.userService = userService;
        }
    }
}
