using SaveUp.Services.Http;

namespace SaveUp.ViewModels
{
    public class AddEntrieViewModel : ViewModelBase
    {
        private readonly HttpEntrieService entrieService;

        public EntriesViewModel Entrie { get; set; }


        public AddEntrieViewModel(HttpEntrieService entrieService)
        {
            this.entrieService = entrieService;
        }

    }
}
