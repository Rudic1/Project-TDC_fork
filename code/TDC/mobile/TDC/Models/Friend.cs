namespace TDC.Models;
public class Friend(string username, string profileImageUrl)
{
    public string Username { get; set; } = username;
    public string ProfileImageUrl { get; set; } = profileImageUrl;
}