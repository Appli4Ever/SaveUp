using CommunityToolkit.Maui.Alerts;
using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class AddEntrieViewModel : ViewModelBase
    {

        private readonly HttpEntrieService entrieService;
        private EntrieViewModel entrie = new();

        public Command AddEntrieCommand { get; set; }

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


        public string Description
        {
            get => this.entrie.Description;
            set
            {
                if (Equals(value, this.entrie.Description)) return;
                this.entrie.Description = value;
                this.OnPropertyChanged();
                this.AddEntrieCommand.ChangeCanExecute();
            }
        }

        public double Amount
        {
            get => this.entrie.Amount;
            set
            {
                if (Equals(value, this.entrie.Amount)) return;
                this.entrie.Amount = value;
                this.OnPropertyChanged();
                this.AddEntrieCommand.ChangeCanExecute();
            }
        }

        public AddEntrieViewModel(HttpEntrieService entrieService, UserIdentity user)
        {
            this.entrieService = entrieService;
            this.AddEntrieCommand = new Command(async () => await this.OnAddEntrie(), this.CanAddEntrie);

            if (user.IsLoggedOut)
            {
                Shell.Current.GoToAsync("//Login");
            }
        }

        public async Task OnAddEntrie()
        {
            this.MainButtonIsEnabled = true;
            var result = await this.entrieService.Create(this.entrie);

            if (result)
            {
                var toast = Toast.Make("Eintrag erstellt");
                await toast.Show();
                this.Description = string.Empty;
                this.Amount = 0;
                await Shell.Current.GoToAsync("//Entries");
            }

            this.MainButtonIsEnabled = true;
        }

        public bool CanAddEntrie()
        {
            return !string.IsNullOrWhiteSpace(this.entrie.Description) && this.entrie.Amount > 0;
        }
    }
}
