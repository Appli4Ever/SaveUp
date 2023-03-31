namespace SaveUp;

public partial class MainPage : ContentPage
{
    public MainPage(UserIdentity userIdentity)
    {
        this.InitializeComponent();
        this.BindingContext = userIdentity;
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Login");
    }
}

