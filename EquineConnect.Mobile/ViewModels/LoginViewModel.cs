using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquineConnect.Mobile.Services;

namespace EquineConnect.Mobile.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _authService;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isLoginPageVisible = true;

        // Command to trigger login
        public IRelayCommand LoginCommand { get; }

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(async () => await Login());
        }

        public async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Email or Password cannot be empty.";
                await Shell.Current.DisplayAlert("Error", "Email or Password cannot be empty", "OK");
                return;
            }

            // Indicate that the login process is in progress
            //SetBusy(true);

            // Call authentication service
            var token = await _authService.Login(Email, Password);

            // Reset the IsBusy flag
            //SetBusy(false);

            if (token != null)
            {
                // Save token and navigate
                Preferences.Set("AuthToken", token);
                IsLoginPageVisible = false;
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                // Show error if login fails
                await Shell.Current.DisplayAlert("Login Failed", "Invalid credentials", "OK");
                Message = "Invalid credentials. Please try again.";
            }
        }
    }
}
