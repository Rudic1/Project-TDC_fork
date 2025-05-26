namespace TDC.Models;
public class Friend(string username)
{
    public string Username { get; set; } = username;

    public ImageSource? ProfileImage { get; set; }
}