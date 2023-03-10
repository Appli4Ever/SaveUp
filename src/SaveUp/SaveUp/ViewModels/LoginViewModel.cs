using CommunityToolkit.Maui.Alerts;
using SaveUp.Models.Enum;
using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly HttpUserService userService;
        private readonly UserIdentity userIdentity;
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

        private bool mainButtonIsEnabled = true;

        public bool MainButtonIsEnabled
        {
            get => this.mainButtonIsEnabled;
            set
            {
                if (Equals(value, this.mainButtonIsEnabled))
                {
                    return;
                }

                this.SetField(ref this.mainButtonIsEnabled, value);
            }
        }


        public Command LoginCommand { get; set; }

        public LoginViewModel(HttpUserService userService, UserIdentity userIdentity)
        {
            this.userService = userService;
            this.userIdentity = userIdentity;

            this.LoginCommand = new Command(async () => await this.OnLogin(), this.CanExecuteLogin);

        }

        public async Task OnLogin()
        {
            this.MainButtonIsEnabled = false;

            var result = await this.userService.Login(this.user);

            switch (result)
            {
                case LoginStatus.Success:
                    var toast = Toast.Make("Login Erfolgreich");
                    await toast.Show();
                    this.Username = string.Empty;
                    this.userIdentity.CheckForLogin();
                    await Shell.Current.GoToAsync("//MainPage");
                    break;
                case LoginStatus.Faild:
                    toast = Toast.Make("Login Fehlgeschlagen");
                    await toast.Show();
                    break;
                case LoginStatus.Blocked:
                    toast = Toast.Make("Login Blockiert");
                    await toast.Show();
                    break;
            }

            this.MainButtonIsEnabled = true;
            this.Password = string.Empty;
        }


        public bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(this.user.Username) && !string.IsNullOrWhiteSpace(this.user.Password);
        }
    }
}
