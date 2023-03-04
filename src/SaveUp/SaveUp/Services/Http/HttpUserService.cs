using System.Net.Http.Headers;
using System.Net.Http.Json;
using SaveUp.Models.Enum;
using SaveUp.Models.ViewModels;

namespace SaveUp.Services.Http
{
    public class HttpUserService
    {
        private readonly HttpClient httpClient;

        public HttpUserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("muellersimon.internet-box.ch");
        }

        public async Task<LoginStatus> Login(UserViewModel model)
        {
            try
            {

                var result = await this.httpClient.PostAsJsonAsync("api/User/Login", model);

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadFromJsonAsync<TokenViewModel>();

                    if (content.LoginStatus == LoginStatus.Success)
                    {
                        this.httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", content.Token);

                        return content.LoginStatus;
                    }
                }

                return LoginStatus.Faild;
            }
            catch
            {
                return LoginStatus.Faild;
            }
        }

        public async Task<bool> Register(UserViewModel model)
        {
            try
            {
                var result = await this.httpClient.PostAsJsonAsync("api/User/CreateUser", model);

                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ChangePassword(PasswordViewModel model)
        {
            try
            {
                var result = await this.httpClient.PostAsJsonAsync("api/User/ChangePassword", model);

                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
