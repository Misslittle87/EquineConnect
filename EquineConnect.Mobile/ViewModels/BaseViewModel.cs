using CommunityToolkit.Mvvm.ComponentModel;

namespace EquineConnect.Mobile.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        public bool isBusy;

        // Property for the title of the current page
        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public string message;

        // Computed property to indicate the inverse of IsBusy
        public bool IsNotBusy => !IsBusy;

        // Optional: Common logic for loading operations, setting isBusy
        public void SetBusy(bool isBusy)
        {
            IsBusy = isBusy;
        }
    }
}
