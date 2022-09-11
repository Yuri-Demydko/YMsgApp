using YMsgApp.AuthServices;
using YMsgApp.Enums;
using YMsgApp.Models.Entities;

namespace YMsgApp;

public partial class LoginPage : ContentPage
{
	private readonly AuthService _authService;
	private bool _loggedIn = false;

	public LoginPage(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;
	
	}


	private void LoginPage_OnLoaded(object sender, EventArgs e)
	{
		Shell.Current.Navigating += (async (sender, args) =>
		{
			if (_loggedIn) return;
			args.Cancel();
			var result = await DisplayActionSheet("Exit from YMsg?", "Cancel", "Yes");
			if (result == "Yes")
			{
				Application.Current!.Quit();
			}
		});
	}

	private async void LoginBtn_OnClicked(object sender, EventArgs e)
	{
		var result = await _authService.LoginAsync(this.UsernameEntry.Text, this.PasswordEntry.Text);
		switch (result)
		{
			case LoginResult.Success:
				_loggedIn = true;
				await Shell.Current.GoToAsync("..");
				break;
			case LoginResult.WrongData:
				await AnnounceValidationError("Invalid password");
				break;
			case LoginResult.NotExist:
				await AnnounceValidationError("User with such username doesn't exist");
				break;
			case LoginResult.ConnectionError:
				await AnnounceValidationError("No internet connection");
				break;
		}
	}

	private async Task AnnounceValidationError(string error)
	{
		await this.ValidationErrorsLabel.FadeTo(0.0,250U, Easing.BounceIn);

		this.ValidationErrorsLabel.Text = error;
		
		await this.ValidationErrorsLabel.FadeTo(1.0,250U, Easing.BounceIn);
	}
	

	private void LoginData_OnTextChanged(object sender, TextChangedEventArgs e)
	{
		this.LoginBtn.IsEnabled = !(string.IsNullOrWhiteSpace(this.UsernameEntry?.Text)||
		                            string.IsNullOrWhiteSpace(this.PasswordEntry?.Text));
	}
}

