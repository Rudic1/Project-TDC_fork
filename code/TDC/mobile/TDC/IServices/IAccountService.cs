using TDC.Models;

namespace TDC.IRepository;
public interface IAccountService
{
    public Task<Account> GetAccountByUsername(string username);
    public Task<bool> CreateAccount(Account account, string password);
    public Task UpdateDescription(string description, string username);
    public Task<bool> UpdateEmail(string email, string username);
    public Task<bool> UpdatePassword(string password, string username);
    public Task<bool> UpdateUsername(string newUsername, string oldUsername);
    public Task<bool> DeleteAccount(string username);
    public Task<bool> UsernameIsTaken(string username);
    public Task<bool> EmailIsTaken(string email);
    public Task<bool> AuthenticateUserLogin(string username, string password);
    public Task<bool> AuthenticateUserLoginWithEmail(string username, string password);

}
