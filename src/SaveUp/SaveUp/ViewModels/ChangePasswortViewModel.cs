using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;
using Toast = CommunityToolkit.Maui.Alerts.Toast;

namespace SaveUp.ViewModels
{
    public class ChangePasswortViewModel : ViewModelBase
    {
        private readonly HttpUserService userService;

        public Command PasswordChangeCommand { get; set; }

        public PasswordViewModel NewPasword { get; set; }

        public ChangePasswortViewModel(HttpUserService userService)
        {
            this.userService = userService;
            this.PasswordChangeCommand = new Command(async () => await this.OnPasswordChange(), this.CanChangePassword);
        }

        public async Task OnPasswordChange()
        {
            var result = await this.userService.ChangePassword(this.NewPasword);

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
            return !string.IsNullOrWhiteSpace(this.NewPasword.VerifiyPassword) && this.NewPasword.VerifiyPassword == this.NewPasword.Password;
        }

    }
}
