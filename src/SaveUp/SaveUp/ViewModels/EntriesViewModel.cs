using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using SaveUp.Models.ViewModels;
using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class EntriesViewModel : ViewModelBase
    {
        private readonly HttpEntrieService entrieService;

        public ObservableCollection<EntrieViewModel> SelectedEntrie { get; set; } = new();

        public Command DeleteEntrieCommand { get; set; }

        public ObservableCollection<EntrieViewModel> Entries { get; set; } = new();

        public EntriesViewModel(HttpEntrieService entrieService)
        {
            this.entrieService = entrieService;
            this.DeleteEntrieCommand = new Command(async () => await this.OnEntireDelete(), this.CanDeleteEntrie);
        }

        public async Task OnEntireLoad()
        {
            var result = await this.entrieService.All();
            this.Entries.Clear();
            foreach (var entrieViewModel in result)
            {
                this.Entries.Add(entrieViewModel);
            }
        }

        public async Task OnEntireDelete()
        {
            if (!this.SelectedEntrie.Any())
            {
                return;
            }

            var result = await this.entrieService.DeleteRange(this.SelectedEntrie);

            if (result)
            {
                var toast = Toast.Make("Eintrag gelöscht");
                await toast.Show();
                await this.OnEntireLoad();
            }
            else
            {
                var toast = Toast.Make("Fehlgeschlagen");
                await toast.Show();
            }
        }

        public bool CanDeleteEntrie()
        {
            return true;
        }

    }
}
