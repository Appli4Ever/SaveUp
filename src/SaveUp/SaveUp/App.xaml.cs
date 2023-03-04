namespace SaveUp;

public partial class App : Application
{
    public App(AppShell appShell)
    {
        this.InitializeComponent();

        this.MainPage = appShell;

        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {
            Console.WriteLine(error);
        };
    }
}
