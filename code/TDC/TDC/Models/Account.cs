namespace TDC.Models;
public class Account: Profile
{
    private string Email { get; set; }
    private string Password { get; set; }
    private List<long> Friends { get; set; } 
    private List<long> Requests { get; set; }

    #region constructors
    public Account(long userId, string username, string description, string email, string password, List<long> friends, List<long> requests): base(userId, username, description)
    {
        Email = email;
        Password = password;
        Friends = friends;
        Requests = requests;
    }
    #endregion
}

