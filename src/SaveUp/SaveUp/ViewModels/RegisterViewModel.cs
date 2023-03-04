using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly HttpUserService userService;
        public Command RegisterCommand { get; set; }
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
                this.RegisterCommand.ChangeCanExecute();
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
                this.RegisterCommand.ChangeCanExecute();
            }
        }

        public RegisterViewModel(HttpUserService userService)
        {
            this.userService = userService;

            this.RegisterCommand = new Command(async () => await this.OnRegister(), this.CanExecuteRegister);
        }


        public async Task OnRegister()
        {
            var result = await this.userService.Register(this.user);

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

        public bool CanExecuteRegister()
        {
            return !string.IsNullOrWhiteSpace(this.user.Username) && !string.IsNullOrWhiteSpace(this.user.Password);
        }

    }
}
