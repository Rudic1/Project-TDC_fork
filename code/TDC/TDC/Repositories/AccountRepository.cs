using TDC.Models;

namespace TDC.Repositories;
public class AccountRepository
{
    private readonly List<Account> accounts;
    private readonly string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.."));
    private readonly string filePath;

    /* ------------------ FOR TESTING -------------------
     * CSV STRUCTURE: Accounts/UserID/
     * lists.csv
     * friends.csv
     * requests.csv
     * info.csv
     * TO-DO: REMOVE ONCE DATABASE IS IMPLEMENTED
     */

    #region constructors
    public AccountRepository()
    {
        filePath = Path.Combine(projectPath, "Accounts");

        #if ANDROID
            string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // create directory if not doesn't exist already
            }

            filePath = Path.Combine(directoryPath, "Accounts");
        #endif
        accounts = new List<Account>();
        LoadAllAccountsFromFile();
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

    #region privates
    private void SaveAccountsToFile()
    {
        
    }

    private void LoadAllAccountsFromFile()
    {

    }
    #endregion
}
