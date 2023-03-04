using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Maui.Alerts;
using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class AddEntrieViewModel : ViewModelBase
    {

        private readonly HttpEntrieService entrieService;

        public Command AddEntrieCommand { get; set; }

        public EntrieViewModel Entrie { get; set; }


        public AddEntrieViewModel(HttpEntrieService entrieService)
        {
            this.entrieService = entrieService;
            this.AddEntrieCommand = new Command(async () => await this.OnAddEntrie(), this.CanAddEntrie);
        }

        public async Task OnAddEntrie()
        {
            var result = await this.entrieService.Create(this.Entrie);

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
            if (Validator.TryValidateObject(this.Entrie, new ValidationContext(this.Entrie, null, null), new List<ValidationResult>()))
            {
                return true;
            }

            return false;
        }

    }
}
