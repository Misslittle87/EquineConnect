using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using EquineConnect.Mobile.Services;
using EquineConnect.Mobile.ViewModels;
using EquineConnect.Mobile.Views;
using Microsoft.Extensions.Logging;

namespace EquineConnect.Mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit().UseMauiCommunityToolkitCore()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<MainPage>();
        //builder.Services.AddSingleton<CalendarPage>();
        //builder.Services.AddSingleton<PersonPage>();
        //builder.Services.AddSingleton<PersonDetailPage>();
        //builder.Services.AddSingleton<DetailPage>();
        //builder.Services.AddSingleton<HorsePage>();
        builder.Services.AddSingleton<RegistrationPage>();
        //builder.Services.AddSingleton<HorseDetailPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<RegistrationPage>();
        builder.Services.AddSingleton<StartPage>();

        builder.Services.AddSingleton<MainViewModel>();
        //builder.Services.AddSingleton<CalendarViewModel>();
        //builder.Services.AddSingleton<PersonViewModel>();
        //builder.Services.AddSingleton<PersonDetailViewModel>();
        //builder.Services.AddSingleton<HorseViewModel>();
        //builder.Services.AddSingleton<HorseDetailViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<RegistrationViewModel>();

        builder.Services.AddSingleton<AuthService>();

        builder.Services.AddSingleton(sp =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            return new HttpClient(handler)
            {
                BaseAddress = new Uri("https://10.0.2.2:7171/")
            };
        });


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
