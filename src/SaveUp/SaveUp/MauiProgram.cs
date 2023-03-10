using System.Net;
using System.Net.Http.Headers;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SaveUp.Pages;
using SaveUp.Services.Http;
using SaveUp.ViewModels;

namespace SaveUp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        Uri baseAddress;

#if DEBUG
        baseAddress = new Uri("http://localhost:5021/");
#else
        baseAddress = new Uri("http://muellersimon.internet-box.ch");
#endif

        builder.Services.AddTransient<UserIdentity>();
        builder.Services.AddTransient(e => new HttpClient(new HttpInterceptorService())
        {
            BaseAddress = baseAddress,
            DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("JWT", "")) }
        });

        builder.Services.AddHttpService();
        builder.Services.AddViewModels();
        builder.Services.AddPages();

#if DEBUG
        builder.Logging.AddDebug();
#endif


        var host = builder.Build();

        return host;
    }
}
