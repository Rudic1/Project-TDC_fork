using TDC.IService;
using TDC.Services;

namespace TDC.Views.FriendRequests;

public partial class FriendRequestsPage : ContentPage
{
    private readonly IFriendService _friendService;
    private readonly UserService _userService;

    public FriendRequestsPage(IFriendService friendService, UserService userService)
    {
        InitializeComponent();
        _friendService = friendService;
        _userService = userService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var username = _userService.CurrentUser?.Username;
        if (string.IsNullOrWhiteSpace(username)) return;

        var requests = await _friendService.GetFriendRequestsForUser(username);
        IncomingRequestsView.ItemsSource = requests;
    }

    private async void Accept_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is string requester)
        {
            await _friendService.AcceptFriendRequest(_userService.CurrentUser!.Username, requester);
            await DisplayAlert("Success", "Friend request accepted.", "OK");
            OnAppearing(); // Refresh list
        }
    }

    private async void Decline_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is string requester)
        {
            await _friendService.DenyFriendRequest(_userService.CurrentUser!.Username, requester);
            await DisplayAlert("Success", "Friend request declined.", "OK");
            OnAppearing(); // Refresh list
        }
    }

    private async void SendRequest_Clicked(object sender, EventArgs e)
    {
        try
        {
            var receiver = UsernameEntry.Text?.Trim();
            var senderUsername = _userService.CurrentUser!.Username;

            if (string.IsNullOrWhiteSpace(receiver))
            {
                await DisplayAlert("Error", "Please enter a valid username.", "OK");
                return;
            }

            if (receiver.Equals(senderUsername, StringComparison.OrdinalIgnoreCase))
            {
                await DisplayAlert("Error", "You cannot send a request to yourself.", "OK");
                return;
            }

            var exists = await _friendService.AccountExists(receiver);
            if (!exists)
            {
                await DisplayAlert("Error", $"User '{receiver}' does not exist.", "OK");
                return;
            }

            await _friendService.SendFriendRequest(senderUsername, receiver);
            await DisplayAlert("Request Sent", $"You sent a request to {receiver}.", "OK");
            UsernameEntry.Text = string.Empty;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Could not send friend request.", "OK");
            Console.WriteLine("Error sending request: " + ex);
        }
    }
}