using System.Net;
using CommunityToolkit.Maui.Alerts;

namespace SaveUp.Services.Http
{
    public class HttpInterceptorService : HttpClientHandler
    {
        private readonly UserIdentity user;

        public HttpInterceptorService()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        {
                            var toast = Toast.Make("Nicht Gefunden :(");
                            await toast.Show(cancellationToken);
                            break;
                        }
                    case HttpStatusCode.Unauthorized:
                        {

                            Preferences.Remove("JWT");
                            Preferences.Remove("Username");
                            var toast = Toast.Make("Sie sind nicht Angemeldet");
                            await toast.Show(cancellationToken);
                            await Shell.Current.GoToAsync("//Login");
                            break;
                        }
                    default:
                        {
                            var toast = Toast.Make("Etwas ging Schief :(\nVersuche es später nochmals");
                            await toast.Show(cancellationToken);
                            break;
                        }
                }
            }

            return response;

        }
    }
}
