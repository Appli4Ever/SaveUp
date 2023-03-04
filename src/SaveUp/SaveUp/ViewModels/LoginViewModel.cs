using CommunityToolkit.Maui.Alerts;
using SaveUp.Models.Enum;
using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly HttpUserService userService;
        private UserViewModel user = new();

        public string Password
        {
            get => this.user.Password;
            set
            {

                if (Equals(value, this.user.Password))
                {
                    return;
                }

                this.user.Password = value;
                this.OnPropertyChanged();
                this.LoginCommand.ChangeCanExecute();
            }
        }

        public string Username
        {
            get => this.user.Username;
            set
            {
                if (Equals(value, this.user.Username))
                {
                    return;
                }

                this.user.Username = value;
                this.OnPropertyChanged();
                this.LoginCommand.ChangeCanExecute();
            }
        }

        public Command LoginCommand { get; set; }

        public LoginViewModel(HttpUserService userService)
        {
            this.userService = userService;

            this.LoginCommand = new Command(async () => await this.OnLogin(), this.CanExecuteLogin);

        }

        public async Task OnLogin()
        {
            var result = await this.userService.Login(this.user);

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


        public bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(this.user.Username) && !string.IsNullOrWhiteSpace(this.user.Password);
        }
    }
}
