using EquineConnect.Mobile.Views;

namespace EquineConnect.Mobile;

public partial class AppShell : Shell
{
    public Command LogoutCommand { get; }
    public AppShell()
    {
        InitializeComponent();

        //Routing.RegisterRoute(nameof(PersonDetailPage), typeof(PersonDetailPage));
        //Routing.RegisterRoute(nameof(HorseDetailPage), typeof(HorseDetailPage));
        Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

        LogoutCommand = new Command(async () => await Logout());
        BindingContext = this;
    }

    public async Task Logout()
    {
        Preferences.Remove("AuthToken"); // Ta bort autentiseringstoken
        await Shell.Current.GoToAsync("//LoginPage"); // Navigera till inloggningssidan
    }
}
