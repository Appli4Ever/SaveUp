using SaveUp.ViewModels;

namespace SaveUp.Pages;

public partial class Login : ContentPage
{
    public Login(LoginViewModel loginRegisterViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = loginRegisterViewModel;
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Register");
    }
}