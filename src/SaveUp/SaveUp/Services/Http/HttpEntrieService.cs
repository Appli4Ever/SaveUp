using System.Net.Http.Json;
using SaveUp.Models.ViewModels;

namespace SaveUp.Services.Http
{
    public class HttpEntrieService
    {
        private readonly HttpClient httpClient;

        public HttpEntrieService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> Create(EntrieViewModel model)
        {
            try
            {
                var result = await this.httpClient.PostAsJsonAsync("api/Entrie/Create", model);

                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteRange(IEnumerable<EntrieViewModel> model)
        {
            try
            {
                var idList = model.Select(e => e.Id).ToArray();

                var result = await this.httpClient.PostAsJsonAsync($"api/Entrie/Delete", idList);

                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<EntrieViewModel>> All()
        {
            try
            {
                var result = await this.httpClient.GetFromJsonAsync<List<EntrieViewModel>>("api/Entrie/All");

                return result;
            }
            catch
            {
                return new List<EntrieViewModel>();
            }
        }
    }
}
