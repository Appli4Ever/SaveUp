namespace SaveUp.Pages
{
    public static class PagesExtension
    {
        public static void AddPages(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<AppShell>();
            serviceCollection.AddTransient<AddEntrie>();
            serviceCollection.AddTransient<ChangePassword>();
            serviceCollection.AddTransient<Entries>();
            serviceCollection.AddTransient<Login>();
            serviceCollection.AddTransient<Register>();
        }
    }
}
