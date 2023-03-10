using System.Net.Http.Headers;
using System.Net.Http.Json;
using SaveUp.Models.Enum;
using SaveUp.Models.ViewModels;

namespace SaveUp.Services.Http
{
    public class HttpUserService
    {
        private readonly HttpClient httpClient;
        private readonly UserIdentity user;

        public HttpUserService(HttpClient httpClient, UserIdentity user)
        {
            this.httpClient = httpClient;
            this.user = user;
        }

        public async Task<LoginStatus> Login(UserViewModel model)
        {
            try
            {
                var result = await this.httpClient.PostAsJsonAsync("api/User/Login", model);

                if (!result.IsSuccessStatusCode)
                {
                    return LoginStatus.Faild;
                }

                var content = await result.Content.ReadFromJsonAsync<TokenViewModel>();

                if (content.LoginStatus == LoginStatus.Success)
                {
                    this.httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", content.Token);

                    this.user.Username = model.Username;
                    this.user.IsLoggedIn = true;
                }

                return content.LoginStatus;

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
