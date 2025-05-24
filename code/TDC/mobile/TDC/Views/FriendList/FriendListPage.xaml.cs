using TDC.IService;

namespace TDC.Views.FriendList;

public partial class FriendListPage : ContentPage
{
    private readonly IFriendService _friendService;
    private readonly Services.UserService _userService;

    public FriendListPage(IFriendService friendService, Services.UserService userService)
    {
        InitializeComponent();

        _friendService = friendService;
        _userService = userService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_userService.CurrentUser != null)
        {
            var friends = await _friendService.GetFriendsForUser(_userService.CurrentUser.Username);
            FriendsCollectionView.ItemsSource = friends;
        }
    }
}