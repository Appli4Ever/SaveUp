using SaveUp.ViewModels;

namespace SaveUp.Pages;

public partial class ChangePassword : ContentPage
{
    public ChangePassword(ChangePasswortViewModel changePasswortViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = changePasswortViewModel;
    }
}