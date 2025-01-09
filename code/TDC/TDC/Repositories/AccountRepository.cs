using TDC.Models;

namespace TDC.Repositories;
public class AccountRepository
{
    private List<Account> accounts;

    #region constructors
    public AccountRepository()
    {
        accounts = new List<Account>();
    }
    #endregion

    #region getters & setters
    public void AddAccount(Account account)
    {
        accounts.Add(account);
    }

    public void RemoveAccount(Account account)
    {
        accounts.Remove(account);
    }

    public List<Account> GetAccounts()
    {
        return accounts;
    }
    #endregion

    public Profile? GetAccountById(int id)
    {
        return null;
    }
}
