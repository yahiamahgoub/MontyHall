using MontyHallLib;
using MontyHallUI.ViewModels;

namespace MontyHallUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddScoped<IMontyHall, MontyHall>();
		builder.Services.AddSingleton<MontyHallViewModel>();
		builder.Services.AddSingleton<MainPage>();

		return builder.Build();
	}
}
