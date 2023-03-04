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

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
