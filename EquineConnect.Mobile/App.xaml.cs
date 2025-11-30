using Microsoft.Extensions.DependencyInjection;

namespace EquineConnect.Mobile;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new AppShell();
    }

	//protected override Window CreateWindow(IActivationState? activationState)
	//{
	//	return new Window(new AppShell());
	//}
}