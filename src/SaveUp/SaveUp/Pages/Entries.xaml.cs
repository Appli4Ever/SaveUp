using SaveUp.ViewModels;

namespace SaveUp.Pages;

public partial class Entries : ContentPage
{
    public event EventHandler OnApperingPage;

    public Entries(EntriesViewModel entriesViewModel)
    {
        this.InitializeComponent();
        this.OnApperingPage += async (s, e) => await entriesViewModel.OnEntireLoad();
        this.BindingContext = entriesViewModel;
    }


    protected override void OnAppearing()
    {
        this.OnApperingPage.Invoke(this, EventArgs.Empty);
        base.OnAppearing();
    }
}