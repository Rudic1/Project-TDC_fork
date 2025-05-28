using TDC.IService;
using TDC.Models;
using TDC.Services;

namespace TDC.Views.FriendList;

public partial class FriendListPage : ContentPage
{
    private readonly IFriendService _friendService;
    private readonly IService.ICharacterService _characterService;
    private readonly Services.UserService _userService;

    public FriendListPage(IFriendService friendService, ICharacterService characterService, Services.UserService userService)
    {
        InitializeComponent();

        _friendService = friendService;
        _characterService = characterService;
        _userService = userService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_userService.CurrentUser != null)
        {
            var usernames = await _friendService.GetFriendsForUser(_userService.CurrentUser.Username);
            var defaultImageUrl = await _characterService.GetDefaultCharacterImage();

            var friends = usernames.Select(username =>
            {
                var friend = new Friend(username);
                friend.ProfileImage = ImageSource.FromUri(new Uri(defaultImageUrl));
                return friend;
            }).ToList();

            FriendsCollectionView.ItemsSource = friends;
        }
    }

    private async void OnRemoveFriendClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string friendUsername)
        {
            bool confirm = await DisplayAlert("Remove Friend", $"Do you really want to remove {friendUsername}?", "Yes", "Cancel");
            if (!confirm) return;

            if (_userService.CurrentUser != null)
            {
                await _friendService.RemoveFriend(_userService.CurrentUser.Username, friendUsername);

                OnAppearing();
            }
        }
    }
}