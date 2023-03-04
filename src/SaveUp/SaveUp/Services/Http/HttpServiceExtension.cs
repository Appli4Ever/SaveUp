namespace SaveUp.Services.Http
{
    public static class HttpServiceExtension
    {
        public static void AddHttpService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<HttpUserService>();
            serviceCollection.AddSingleton<HttpEntrieService>();
        }
    }
}
