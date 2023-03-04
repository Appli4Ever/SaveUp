using SaveUp.ViewModels;

namespace SaveUp.Pages;

public partial class AddEntrie : ContentPage
{

    public AddEntrie(AddEntrieViewModel addEntrieViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = addEntrieViewModel;
    }
}