using SaveUp.ViewModels;

namespace SaveUp.Pages;

public partial class Register : ContentPage
{
    public Register(RegisterViewModel loginRegisterViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = loginRegisterViewModel;
    }
}