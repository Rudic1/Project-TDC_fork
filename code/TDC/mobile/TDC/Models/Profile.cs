namespace TDC.Models;
public class Profile(string username, string description)
{
    public string Username { get; set; } = username;
    public string Description { get; set; } = description;
}

