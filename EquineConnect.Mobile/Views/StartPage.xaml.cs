namespace EquineConnect.Mobile.Views;

public partial class StartPage : ContentPage
{
    public StartPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (Preferences.ContainsKey("AuthToken"))
        {
            await Shell.Current.GoToAsync("//app");
        }
        else
        {
            await Shell.Current.GoToAsync("//login");
        }
    }
}