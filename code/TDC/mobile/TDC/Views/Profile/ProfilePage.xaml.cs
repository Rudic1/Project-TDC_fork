using TDC.Services;

namespace TDC.Views.Profile;

public partial class ProfilePage : ContentPage
{
    private readonly Services.UserService _userService;

    public ProfilePage()
	{
		InitializeComponent();

        _userService = App.Services.GetRequiredService<Services.UserService>();
        LoadUserData(_userService);
    }

    private void LoadUserData(UserService userService)
    {
        if (userService.IsLoggedIn)
        {
            UserNameLabel.Text = userService.CurrentUser!.Username;
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