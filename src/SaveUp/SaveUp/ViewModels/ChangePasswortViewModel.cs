using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class ChangePasswortViewModel : ViewModelBase
    {
        private readonly HttpUserService userService;

        public PasswordViewModel Newpasword { get; set; }

        public ChangePasswortViewModel(HttpUserService userService)
        {
            this.userService = userService;
        }


    }
}
