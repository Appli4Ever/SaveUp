using CommunityToolkit.Maui;
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

        builder.Services.AddPages();
        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddHttpService();
        builder.Services.AddViewModels();

#if DEBUG
        builder.Logging.AddDebug();
#endif


        var host = builder.Build();
        var httpClient = host.Services.GetRequiredService<HttpClient>();


#if DEBUG
        httpClient.BaseAddress = new Uri("http://localhost:5021/");
#else
        httpClient.BaseAddress = new Uri("http://muellersimon.internet-box.ch");
#endif

        return host;
    }
}
