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
            var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            filePath = Path.Combine(directoryPath, "Accounts");
        #endif
        accounts = new List<Account>();
        //LoadAllAccounts();
    }
    #endregion

    #region publics
    public void AddAccount(Account account)
    {
        accounts.Add(account);
        SaveAccountToFile(account);
    }

    public void RemoveAccount(Account account)
    {
        accounts.Remove(account);
        DeleteAccount(account.UserId);
    }

    public List<Account> GetAllAccounts()
    {
        // testing only
        #if ANDROID
            return GetDummyAccounts();
        #endif
        return accounts;
    }

    public Account? GetAccountById(long id)
    {
        return accounts.FirstOrDefault(acc => acc.UserId.Equals(id));
    }

    public void SaveAllAccounts()
    {
        foreach (var acc in accounts)
        {
            SaveAccountToFile(acc);
        }
    }

    public void SaveAccountToFile(Account acc)
    {
        if (!Directory.Exists(filePath + "/" + acc.UserId))
        {
            Directory.CreateDirectory(filePath + "/" + acc.UserId);
        }
        
        SaveGeneralInfo(acc, filePath + "/" + acc.UserId + "/info.csv");
        /*SaveAssociatedLists(acc.GetLists(), filePath + "/" + acc.UserId + "/lists.csv");
        SaveFriendList(acc.GetFriendList(), filePath + "/" + acc.GetId() + "/friends.csv");
        SaveRequestList(acc.GetRequests(), filePath + "/" + acc.GetId() + "/requests.csv");*/
    }

    #endregion

    #region privates


    private static void SetListsForAccount(Account acc, string fullPath)
    {
        
    }

    private static void SetFriendsForAccount(Account acc, string fullPath)
    {
        
    }

    private static void SetRequestsForAccount(Account acc, string fullPath)
    {
    }


    private void DeleteAccount(long id)
    {
        if (Directory.Exists(filePath + "/" + id))
        {
            Directory.Delete(filePath + "/" + id);
        }
    }

    private static void SaveGeneralInfo(Account acc, string fullPath)
    {
        
    }

    private static void SaveFriendList(List<string> friends, string fullPath)
    {
        
    }

    private static void SaveRequestList(List<string> requests, string fullPath)
    {
        
    }

    private static void SaveAssociatedLists(List<string> lists, string fullPath)
    {
        
    }

    // TO-DO: remove once database is implemented, only for android testing
    private static List<Account> GetDummyAccounts()
    {
        var dummyAccounts = new List<Account>();
        var acc1 = new Account(1, "acc1", "description1", "mail 1", "pw1", new Character("", "", 0, new CharacterStats(0, 0, 0)), [], []);
        dummyAccounts.Add(acc1);

        var acc2 = new Account(2, "acc2", "description2", "mail 2", "pw2", new Character("", "", 0, new CharacterStats(0, 0, 0)), [1], []);
        dummyAccounts.Add(acc2);

        var acc3 = new Account(3, "acc3", "description3", "mail 3", "pw3", new Character("", "", 0, new CharacterStats(0, 0, 0)), [2], [1]);
        dummyAccounts.Add(acc3);
        return dummyAccounts;
    }
    #endregion
}
