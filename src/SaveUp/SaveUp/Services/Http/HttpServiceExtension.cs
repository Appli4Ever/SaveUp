namespace SaveUp.Services.Http
{
    public static class HttpServiceExtension
    {
        public static void AddHttpService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<HttpUserService>();
            serviceCollection.AddTransient<HttpEntrieService>();
        }
    }
}
