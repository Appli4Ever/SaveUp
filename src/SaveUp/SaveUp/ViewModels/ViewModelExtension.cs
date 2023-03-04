namespace SaveUp.ViewModels
{
    public static class ViewModelExtension
    {
        public static void AddViewModels(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<RegisterViewModel>();
            serviceCollection.AddTransient<AddEntrieViewModel>();
            serviceCollection.AddTransient<ChangePasswortViewModel>();
            serviceCollection.AddTransient<EntriesViewModel>();
            serviceCollection.AddTransient<LoginViewModel>();
        }
    }
}
