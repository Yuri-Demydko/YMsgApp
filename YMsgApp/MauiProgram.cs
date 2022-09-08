using System.Reflection;
using Microsoft.Extensions.Configuration;
using YMsgApp.CacheServices;
using YMsgApp.Configurations;
using YMsgApp.DataRestServices.Ping;

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
		
		var executingAssembly = Assembly.GetExecutingAssembly();
		using var stream = executingAssembly.GetManifestResourceStream("YMsgApp.appsettings.json");

		var config = new ConfigurationBuilder()
			.AddJsonStream(stream)
			.Build();


		builder.Configuration.AddConfiguration(config);
		
		builder.Services.ConfigureSQLiteCache(config);

		builder.Services.AddScoped<MessageSQLiteCacheService>();

		builder.Services.AddSingleton<IPingRestService>(new PingRestService("https://ymessage.space"));

		builder.Services.AddSingleton<MainPage>();
		
		return builder.Build();
	}
}
