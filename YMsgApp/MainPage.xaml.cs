using YMsgApp.DataServices.Ping;

namespace YMsgApp;

public partial class MainPage : ContentPage
{

	private readonly IPingRestService _pingRestService;

	public MainPage(IPingRestService pingRestService)
	{
		_pingRestService = pingRestService;
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

