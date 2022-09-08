using YMsgApp.CacheServices;
using YMsgApp.DataRestServices.Ping;

namespace YMsgApp;

public partial class MainPage : ContentPage
{

	private readonly IPingRestService _pingRestService;
	private readonly MessageSQLiteCacheService _cache;

	public MainPage(IPingRestService pingRestService, MessageSQLiteCacheService cache)
	{
		_pingRestService = pingRestService;
		_cache = cache;
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		var res = await _pingRestService.PingAsync();
		if (!res.IsSuccess)
		{
			Label.Text = $"Ping error:\n{res.ResponseMessage}";
			return;
		}

		Label.Text = $"Ping succeed!\n" +
		             $"Today: {res?.ResponseObject?.CurrentServerDateTime:u}\n" +
		             $"Message: {res?.ResponseObject?.Message}";
	}
}

