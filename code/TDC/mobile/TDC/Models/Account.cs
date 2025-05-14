namespace TDC.Models;
public class Account(
    string username,
    string description,
    string email)
    : Profile(username, description)
{
    public string Email { get; set; } = email;
}

