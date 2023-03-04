using SaveUp.ViewModels;

namespace SaveUp.Pages;

public partial class Register : ContentPage
{
    public Register(LoginRegisterViewModel loginRegisterViewModel)
    {
        this.InitializeComponent();
        loginRegisterViewModel.IsRegisterViewModle = true;
        this.BindingContext = loginRegisterViewModel;
    }
}