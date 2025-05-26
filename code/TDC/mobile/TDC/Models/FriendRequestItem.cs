namespace TDC.Models;

public class FriendRequestItem
{
    public string Username { get; set; }
    public ImageSource? ProfileImage { get; set; }

    public FriendRequestItem(string username)
    {
        Username = username;
    }
}