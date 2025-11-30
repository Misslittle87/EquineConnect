using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquineConnect.Mobile.Models;
using System.Collections.ObjectModel;

namespace EquineConnect.Mobile.ViewModels
{
    public partial class RegistrationViewModel : BaseViewModel
    {
        public ObservableCollection<User> UserInfo { get; set; } = new ObservableCollection<User>();
        public RegistrationViewModel()
        {

        }

        [ObservableProperty]
        public string userName;
        [ObservableProperty]
        string password;

        [RelayCommand]
        async Task Add()
        {
            if (string.IsNullOrWhiteSpace(userName) && string.IsNullOrWhiteSpace(password))
            {
                await Shell.Current.DisplayAlert("Något gick fel!", "Du måste fylla i användarnamn och lösenord", "OK");
                return;
            }
            else
            {
                //await LoginService.AddUser(userName, password);
            }
        }
    }
}
