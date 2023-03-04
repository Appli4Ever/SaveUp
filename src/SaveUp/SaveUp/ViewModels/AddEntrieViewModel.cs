using System.ComponentModel.DataAnnotations;
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

        public AddEntrieViewModel(HttpEntrieService entrieService)
        {
            this.entrieService = entrieService;
            this.AddEntrieCommand = new Command(async () => await this.OnAddEntrie(), this.CanAddEntrie);
        }

        public async Task OnAddEntrie()
        {
            var result = await this.entrieService.Create(this.entrie);

            if (result)
            {
                var toast = Toast.Make("Eintrag erstellt");
                await toast.Show();
            }
            else
            {
                var toast = Toast.Make("Fehlgeschlagen");
                await toast.Show();
            }
        }

        public bool CanAddEntrie()
        {
            return !string.IsNullOrWhiteSpace(this.entrie.Description) && this.entrie.Amount > 0;
        }
    }
}
