using System.Collections.ObjectModel;
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

        public double TotalAmount => this.Entries.Sum(e => e.Amount);

        public EntriesViewModel(HttpEntrieService entrieService)
        {
            this.entrieService = entrieService;
            this.DeleteEntrieCommand = new Command(async () => await this.OnEntireDelete(), this.CanDeleteEntrie);
        }

        public async Task OnEntireLoad()
        {
            this.Entries.Clear();
            var result = await this.entrieService.All();
            foreach (var entrieViewModel in result)
            {
                this.Entries.Add(entrieViewModel);
            }

            this.OnPropertyChanged(nameof(this.TotalAmount));
            this.OnPropertyChanged(nameof(this.Entries));
        }

        public async Task OnEntireDelete()
        {
            if (!this.SelectedEntrie.Any())
            {
                var toast = Toast.Make("Keine Einträge Ausgewählt");
                await toast.Show();
                return;
            }

            this.MainButtonIsEnabled = false;

            var result = await this.entrieService.DeleteRange(this.SelectedEntrie);

            if (result)
            {
                var toast = Toast.Make("Eintrag gelöscht");
                await toast.Show();
                await this.OnEntireLoad();
            }

            this.MainButtonIsEnabled = true;
        }

        public bool CanDeleteEntrie()
        {
            return true;
        }

    }
}
