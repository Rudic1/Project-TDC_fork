using TDC.Services;

namespace TDC.Views.Profile;

public partial class ProfilePage : ContentPage
{
    private readonly Services.UserService _userService;
    private readonly IService.IAccountService _accountService;
    private readonly IService.ICharacterService _characterService;

    public ProfilePage()
	{
		InitializeComponent();

        _userService = App.Services.GetRequiredService<Services.UserService>();
        _accountService = App.Services.GetRequiredService<IService.IAccountService>();
        _characterService = App.Services.GetRequiredService<IService.ICharacterService>();

        LoadProfileImage();
        LoadUserData();
    }

    private async Task LoadProfileImage()
    {
        try
        {
            var imageUrl = await _characterService.GetDefaultCharacterImage();

            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                ProfileImage.Source = ImageSource.FromUri(new Uri(imageUrl));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Laden des Profilbildes: " + ex.Message);
        }
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

    private async void SaveDescriptionButton_Clicked(object sender, EventArgs e)
    {
        string newDescription = DescriptionEditor.Text ?? string.Empty;
        string username = _userService.CurrentUser!.Username;

        try
        {
            await _accountService.UpdateDescription(newDescription, username);
            await DisplayAlert("Erfolg", "Beschreibung gespeichert.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fehler", "Beim Speichern ist ein Fehler aufgetreten.", "OK");
            Console.WriteLine(ex.Message);
        }
    }

    private async void OpenFriendList_Clicked(object sender, EventArgs e)
    {
        // TODO: Navigation to FriendListPage
        //await Shell.Current.GoToAsync("FriendListPage");
    }
}