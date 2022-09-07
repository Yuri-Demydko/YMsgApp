using YMsgApp.DataServices.Ping;

namespace YMsgApp;

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

		builder.Services.AddSingleton<IPingRestService>(new PingRestService("https://ymessage.space"));

		builder.Services.AddSingleton<MainPage>();
		
		return builder.Build();
	}
}
