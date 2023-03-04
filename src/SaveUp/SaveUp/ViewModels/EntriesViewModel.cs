using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class EntriesViewModel : ViewModelBase
    {
        private readonly HttpEntrieService entrieService;

        public EntrieViewModel SelectedEntrie { get; set; }

        public Command DeleteEntrieCommand { get; set; }

        public ObservableCollection<EntrieViewModel> Entries { get; set; }

        public EntriesViewModel(HttpEntrieService entrieService)
        {
            this.entrieService = entrieService;
            this.DeleteEntrieCommand = new Command(async () => await this.OnEntireDelete(), this.CanDeleteEntrie);
        }

        public async Task OnEntireDelete()
        {
            var result = await this.entrieService.Delete(this.SelectedEntrie);

            if (result)
            {
                var toast = Toast.Make("Eintrag gelöscht");
                await toast.Show();
            }
            else
            {
                var toast = Toast.Make("Fehlgeschlagen");
                await toast.Show();
            }
        }

        public bool CanDeleteEntrie()
        {
            return this.SelectedEntrie is not null;
        }

    }
}
