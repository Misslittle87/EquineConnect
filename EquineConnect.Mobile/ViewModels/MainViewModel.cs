using CommunityToolkit.Mvvm.ComponentModel;
using EquineConnect.Mobile.Models;
using System.Collections.ObjectModel;

namespace EquineConnect.Mobile.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        ObservableCollection<User> _users { get; set; }

        [ObservableProperty]
        DateTime currentDate = DateTime.Now;

        [ObservableProperty]
        public string loginName;
        public MainViewModel()
        {
            _users = new ObservableCollection<User>();
        }
    }
}
