using SaveUp.ViewModels;

namespace SaveUp.Pages;

public partial class Register : ContentPage
{
    public Register(LoginRegisterViewModel loginRegisterViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = loginRegisterViewModel;
    }
}