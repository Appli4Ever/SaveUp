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
            this.httpClient.BaseAddress = new Uri("muellersimon.internet-box.ch");
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

        public async Task<bool> Delete(EntrieViewModel model)
        {
            try
            {
                var result = await this.httpClient.PostAsJsonAsync("api/Entrie/Delete", model);

                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
