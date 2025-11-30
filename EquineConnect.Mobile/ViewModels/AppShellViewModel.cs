using CommunityToolkit.Mvvm.Input;
using EquineConnect.Mobile.Views;

namespace EquineConnect.Mobile.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [RelayCommand]
        async void SignOut()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
