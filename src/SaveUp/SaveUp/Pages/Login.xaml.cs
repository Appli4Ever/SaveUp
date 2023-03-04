using SaveUp.ViewModels;

namespace SaveUp.Pages;

public partial class Login : ContentPage
{
    public Login(LoginViewModel loginRegisterViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = loginRegisterViewModel;
    }
}