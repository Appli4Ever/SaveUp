using SaveUp.ViewModels;

namespace SaveUp.Pages;

public partial class Login : ContentPage
{
    public Login(LoginRegisterViewModel loginRegisterViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = loginRegisterViewModel;
    }
}