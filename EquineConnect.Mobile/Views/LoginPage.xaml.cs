using EquineConnect.Mobile.Services;
using EquineConnect.Mobile.ViewModels;

namespace EquineConnect.Mobile.Views;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _authService;
    public LoginPage(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
        BindingContext = new LoginViewModel(authService);
    }

    private async void OnLoginSuccess()
    {
        Preferences.Set("AuthToken", "some-token-value"); // Spara token
        await Shell.Current.GoToAsync("//MainPage", true); // Rensa historiken
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
    }
}