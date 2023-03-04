using SaveUp.ViewModels;

namespace SaveUp.Pages;

public partial class Entries : ContentPage
{

    public Entries(EntriesViewModel entriesViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = entriesViewModel;
    }
}