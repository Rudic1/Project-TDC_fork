using TDC.Models;

namespace TDC.IRepository;
public interface IAccountRepository
{
    public Account GetAccountByUsername(string username);
    public bool CreateAccount(Account account);
    public Task UpdateDescription(string description, string username);
    public bool UpdateEmail(string email, string username);
    public bool UpdatePassword(string password, string username);
    public bool UpdateUsername(string newUsername, string oldUsername);
    public bool DeleteAccount(string username);
    public bool UsernameIsTaken(string username);
    public bool EmailIsTaken(string email);
    public bool AuthenticateUserLogin(string username, string password);
    public bool AuthenticateUserLoginWithEmail(string username, string password);

}
