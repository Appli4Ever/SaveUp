using CommunityToolkit.Maui.Alerts;
using SaveUp.Models.Enum;
using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class LoginRegisterViewModel : ViewModelBase
    {
        private readonly HttpUserService userService;

        public bool IsRegisterViewModle = false;

        public UserViewModel User { get; set; }

        public Command LoginRegisterCommand { get; set; }

        public LoginRegisterViewModel(HttpUserService userService)
        {
            this.userService = userService;

            if (this.IsRegisterViewModle)
            {
                this.LoginRegisterCommand = new Command(async () => await this.OnRegister(), this.CanExecuteLoginOrRegister);
            }
            else
            {
                this.LoginRegisterCommand = new Command(async () => await this.OnLogin(), this.CanExecuteLoginOrRegister);
            }
        }

        public async Task OnLogin()
        {
            var result = await this.userService.Login(this.User);

            var toast = Toast.Make("");

            switch (result)
            {
                case LoginStatus.Success:
                    toast = Toast.Make("Login Erfolgreich");
                    break;
                case LoginStatus.Faild:
                    toast = Toast.Make("Login Fehlgeschlagen");
                    break;
                case LoginStatus.Blocked:
                    toast = Toast.Make("Login Blockiert");
                    break;
            }
            await toast.Show();
        }

        public async Task OnRegister()
        {
            var result = await this.userService.Register(this.User);

            if (result)
            {
                var toast = Toast.Make("Registrierung Erfolgreich");
                await toast.Show();
            }
            else
            {
                var toast = Toast.Make("Fehlgeschlagen");
                await toast.Show();
            }
        }

        public bool CanExecuteLoginOrRegister()
        {
            return !string.IsNullOrWhiteSpace(this.User.Username) && !string.IsNullOrWhiteSpace(this.User.Password);
        }
    }
}
