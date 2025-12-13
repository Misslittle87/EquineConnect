using EquineConnect.Mobile.ViewModels;

namespace EquineConnect.Mobile.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
