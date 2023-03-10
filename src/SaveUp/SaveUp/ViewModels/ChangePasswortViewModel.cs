using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;
using Toast = CommunityToolkit.Maui.Alerts.Toast;

namespace SaveUp.ViewModels
{
    public class ChangePasswortViewModel : ViewModelBase
    {
        private readonly HttpUserService userService;
        private PasswordViewModel newPasword = new();

        public Command PasswordChangeCommand { get; set; }

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


        public string Password
        {
            get => this.newPasword.Password;
            set
            {
                if (Equals(value, this.newPasword.Password)) return;
                this.newPasword.Password = value;
                this.OnPropertyChanged();
                this.PasswordChangeCommand.ChangeCanExecute();
            }
        }

        public string VerifyPassword
        {
            get => this.newPasword.Password;
            set
            {
                if (Equals(value, this.newPasword.VerifiyPassword)) return;
                this.newPasword.VerifiyPassword = value;
                this.OnPropertyChanged();
                this.PasswordChangeCommand.ChangeCanExecute();
            }
        }

        public ChangePasswortViewModel(HttpUserService userService, UserIdentity user)
        {
            this.userService = userService;
            this.PasswordChangeCommand = new Command(async () => await this.OnPasswordChange(), this.CanChangePassword);

            if (user.IsLoggedOut)
            {
                Shell.Current.GoToAsync("//Login");
            }
        }

        public async Task OnPasswordChange()
        {
            this.MainButtonIsEnabled = false;

            var result = await this.userService.ChangePassword(this.newPasword);

            if (result)
            {
                var toast = Toast.Make("Passwort geändert");
                await toast.Show();

                await Shell.Current.GoToAsync("//MainPage");
            }

            this.Password = string.Empty;
            this.VerifyPassword = string.Empty;

            this.MainButtonIsEnabled = true;
        }

        public bool CanChangePassword()
        {
            return !string.IsNullOrWhiteSpace(this.newPasword.VerifiyPassword) && this.newPasword.VerifiyPassword == this.newPasword.Password;
        }

    }
}
