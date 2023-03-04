using System.Collections.ObjectModel;
using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class EntriesViewModel : ViewModelBase
    {
        private readonly HttpEntrieService entrieService;

        public ObservableCollection<EntriesViewModel> Entries { get; set; }

        public EntriesViewModel(HttpEntrieService entrieService)
        {
            this.entrieService = entrieService;
        }


    }
}
