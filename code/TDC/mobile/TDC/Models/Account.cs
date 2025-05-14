namespace TDC.Models;
public class Account(
    string username,
    string description,
    string email,
    string password,
    List<string> friends,
    List<string> requests)
    : Profile(username, description)
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public List<string> Friends { get; set; } = friends;
    public List<string> Requests { get; set; } = requests;
}

