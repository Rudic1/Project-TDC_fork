using TDC.Services;

namespace TDC.Views.Profile;

public partial class ProfilePage : ContentPage
{
    private readonly Services.UserService _userService;
    private readonly IService.IAccountService _accountService;

    public ProfilePage()
	{
		InitializeComponent();

        _userService = App.Services.GetRequiredService<Services.UserService>();
        _accountService = App.Services.GetRequiredService<IService.IAccountService>();

        LoadUserData();
    }

    private async Task LoadUserData()
    {
        if (_userService.IsLoggedIn)
        {
            string username = _userService.CurrentUser!.Username;
                                  
            var fullAccount = await _accountService.GetAccountByUsername(username);

            UserNameLabel.Text = username;
            DescriptionEditor.Text = fullAccount.Description;
            EmailLabel.Text = fullAccount.Email; 
        }
    }

    private void DescriptionEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        int remaining = 150 - e.NewTextValue?.Length ?? 0;
        CharacterCountLabel.Text = $"{remaining}";
    }

    private async void OpenFriendList_Clicked(object sender, EventArgs e)
    {
        // TODO: Navigation to FriendListPage
        //await Shell.Current.GoToAsync("FriendListPage");
    }
}