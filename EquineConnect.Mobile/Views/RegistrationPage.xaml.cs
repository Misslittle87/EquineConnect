using CommunityToolkit.Maui.Behaviors;
using EquineConnect.Mobile.ViewModels;

namespace EquineConnect.Mobile.Views;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage(RegistrationViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        var entry = new Entry();
        var validStyle = new Style(typeof(Entry));
        validStyle.Setters.Add(new Setter
        {
            Property = Entry.TextColorProperty,
            Value = Colors.Green
        });
        var invalidStyle = new Style(typeof(Entry));
        invalidStyle.Setters.Add(new Setter
        {
            Property = Entry.TextColorProperty,
            Value = Colors.Red
        });
        var requiredStringValidationBehavior = new RequiredStringValidationBehavior
        {
            InvalidStyle = invalidStyle,
            ValidStyle = validStyle,
            Flags = ValidationFlags.ValidateOnValueChanged,
            RequiredString = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
        };
        entry.Behaviors.Add(requiredStringValidationBehavior);
        Content = entry;
    }
}