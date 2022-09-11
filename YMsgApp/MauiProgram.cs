using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using YMsgApp.AuthServices;
using YMsgApp.Background;
using YMsgApp.CacheServices;
using YMsgApp.Configurations;
using YMsgApp.Configurations.SharedConfigurations;
using YMsgApp.DataRestServices.Auth;
using YMsgApp.DataRestServices.Ping;
using YMsgApp.DataRestServices.Profile;
using YMsgApp.Models.Caching.CacheModels;
using YMsgApp.Models.DtoModels.RequestModels;
using YMsgApp.Models.Entities;


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

		
		//add configs
		builder.Configuration.AddConfiguration(config);

		SharedConfiguration.BaseApiAddress = builder.Configuration.GetValue<string>("Api:BaseApiAddress");
		
		SharedConfiguration.DatabaseVersion = builder.Configuration.GetValue<string>("Database:Version");

		var serviceProvider = builder.Services.BuildServiceProvider();
		BackgroundTaskExecutor.ConfigureServiceProvider(serviceProvider);
		
		//Add services
		builder.Services.ConfigureSQLiteCache(config);
		
		builder.Services.AddSingleton<IPingRestService,PingRestService>();
		
		builder.Services.AddScoped<MessageSQLiteCacheService>();

		builder.Services.AddScoped<IAuthRestService, AuthRestService>();

		builder.Services.AddScoped<AuthService>();
		
		builder.Services.AddScoped<ProfileRestService>();

		builder.Services.AddScoped<ProfileCacheService>();
		
		builder.Services.AddScoped<TokenSQLiteCacheService>();

		builder.Services.AddAutoMapper(mapperConf =>
		{
			mapperConf.CreateMap<UserCache, User>()
				.ForMember(r => r.Id, opt => opt.MapFrom(r => r.ExternalId));

		});
		
		//Add pages
		builder.Services.AddScoped<MainPage>();

		

		builder.Services.AddTransient<LoginPage>();

		//start tasks
		BackgroundTaskExecutor.Execute<AuthService>("RefreshTokenJob", TimeSpan.FromSeconds(30));

		return builder.Build();
	}
}
