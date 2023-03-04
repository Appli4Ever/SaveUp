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

        public string Pasword
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

        public string VerifyPasword
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

        public ChangePasswortViewModel(HttpUserService userService)
        {
            this.userService = userService;
            this.PasswordChangeCommand = new Command(async () => await this.OnPasswordChange(), this.CanChangePassword);
        }

        public async Task OnPasswordChange()
        {
            var result = await this.userService.ChangePassword(this.newPasword);

            if (result)
            {
                var toast = Toast.Make("Passwort geändert");
                await toast.Show();
            }
            else
            {
                var toast = Toast.Make("Fehlgeschlagen");
                await toast.Show();
            }
        }

        public bool CanChangePassword()
        {
            return !string.IsNullOrWhiteSpace(this.newPasword.VerifiyPassword) && this.newPasword.VerifiyPassword == this.newPasword.Password;
        }

    }
}
