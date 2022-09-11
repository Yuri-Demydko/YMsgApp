using AutoMapper;
using Microsoft.Maui.LifecycleEvents;
using YMsgApp.CacheServices;
using YMsgApp.Configurations;
using YMsgApp.Configurations.SharedConfigurations;
using YMsgApp.DataRestServices.Ping;
using YMsgApp.Models.Caching.CacheModels;
using YMsgApp.Models.Entities;

namespace YMsgApp;

public partial class MainPage : ContentPage
{
	
	private readonly MessageSQLiteCacheService _messageCache;
	private readonly IMapper _mapper;
	private User _userProfile;
	private readonly ProfileCacheService _profileCacheService;
	private readonly IPingRestService _pingRestService;

	public MainPage(MessageSQLiteCacheService messageCache, ProfileCacheService profileCacheService, IMapper mapper, IPingRestService pingRestService)
	{
		_messageCache = messageCache;
		_profileCacheService = profileCacheService;
		_mapper = mapper;
		_pingRestService = pingRestService;
		InitializeComponent();
		BindingContext = this;
	}

	public User UserProfile
	{
		get => _userProfile;
		set
		{
			_userProfile = value;
			this.UsernameLabel.Text = _userProfile?.DisplayName ?? _userProfile?.UserName??"";
		}
	}


	private async void OnCounterClicked(object sender, EventArgs e)
	{
		
	}

	private async void RedirectToLogin()
	{
		// var navigationParameter = new Dictionary<string, object>()
		// {
		// 	{nameof(LoginModel), new LoginModel()}
		// };

		await Shell.Current.GoToAsync(nameof(LoginPage));
	}
	
	

	private async void MainPage_OnLoaded(object sender, EventArgs e)
	{
		
	}

	private async void MainPage_OnNavigatedTo(object sender, NavigatedToEventArgs e)
	{
		var profileCache = await _profileCacheService.GetProfile();
		if (profileCache == null)
		{
			RedirectToLogin();
		}

		UserProfile = _mapper.Map<User>(profileCache);
	}
}

