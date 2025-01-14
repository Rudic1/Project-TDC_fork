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
        LoadAllAccounts();
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
        DeleteAccount(account.GetId());
    }

    public List<Account> GetAllAccounts()
    {
        return accounts;
    }

    public Account? GetAccountById(string id)
    {
        return accounts.FirstOrDefault(acc => acc.GetId().Equals(id));
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
        if (!Directory.Exists(filePath + "/" + acc.GetId()))
        {
            Directory.CreateDirectory(filePath + "/" + acc.GetId()); // create directory if not doesn't exist already
        }
        
        SaveGeneralInfo(acc, filePath + "/" + acc.GetId() + "/info.csv");
        SaveAssociatedLists(acc.GetLists(), filePath + "/" + acc.GetId() + "/lists.csv");
        SaveFriendList(acc.GetFriendList(), filePath + "/" + acc.GetId() + "/friends.csv");
        SaveRequestList(acc.GetRequests(), filePath + "/" + acc.GetId() + "/requests.csv");
    }

    #endregion

    #region privates
    private void LoadAllAccounts()
    {
        var accountDirs = Directory.GetDirectories(filePath);
        foreach (var dir in accountDirs)
        {
        }
    }

    private Account LoadAccountFromDirectory(string accountPath)
    {
        var reader = new StreamReader(accountPath + "info.csv");
        var accInfo = reader.ReadToEnd().Split(Environment.NewLine)[1].Split(';');

        var acc = new Account(accInfo[0], accInfo[1], accInfo[2], accInfo[3], accInfo[5], new Character()); //TO-DO: Implement Character database
        SetListsForAccount(acc, accountPath + "lists.csv");
        SetFriendsForAccount(acc, accountPath + "friends.csv");
        SetRequestsForAccount(acc, accountPath + "requests.csv");
        return acc;
    }

    private static void SetListsForAccount(Account acc, string fullPath)
    {
        var listRepos = new ListRepository();
        var reader = new StreamReader(fullPath);
        var listIds = reader.ReadToEnd().Split(Environment.NewLine);
        foreach (var id in listIds)
        {
            acc.AddList(id); //TO-DO: secure connected lists are always saved
        }
    }

    private static void SetFriendsForAccount(Account acc, string fullPath)
    {
        var reader = new StreamReader(fullPath);
        var friendIds = reader.ReadToEnd().Split(Environment.NewLine);
        foreach (var id in friendIds)
        {
            acc.AddFriend(id);
        }
    }

    private static void SetRequestsForAccount(Account acc, string fullPath)
    {
        var reader = new StreamReader(fullPath);
        var requestIds = reader.ReadToEnd().Split(Environment.NewLine);
        foreach (var id in requestIds)
        {
            acc.AddFriend(id);
        }
    }


    private void DeleteAccount(string id)
    {
        if (Directory.Exists(filePath + "/" + id))
        {
            Directory.Delete(filePath + "/" + id); // create directory if not doesn't exist already
        }
    }

    private static void SaveGeneralInfo(Account acc, string fullPath)
    {
        var writer = new StreamWriter(fullPath);
        writer.WriteLine("AccountID;Name;Description;E-Mail;Password;CharacterID");
        writer.WriteLine(acc.GetId() + ";" + acc.GetName() + ";" + acc.GetDescription() + ";" + acc.GetEmail() + ";" + acc.GetPassword() + ";" + "default"); //TO-DO: add character database or use different saving method
        writer.Close();
    }

    private static void SaveFriendList(List<string> friends, string fullPath)
    {
        var writer = new StreamWriter(fullPath);
        foreach (var friend in friends)
        {
            writer.WriteLine(friend);
        }
        writer.Close();
    }

    private static void SaveRequestList(List<string> requests, string fullPath)
    {
        var writer = new StreamWriter(fullPath);
        foreach (var request in requests)
        {
            writer.WriteLine(request);
        }
        writer.Close();
    }

    private static void SaveAssociatedLists(List<string> lists, string fullPath)
    {
        var writer = new StreamWriter(fullPath);
        foreach (var list in lists)
        {
            writer.WriteLine(list);
        }
        writer.Close();
    }
    #endregion
}
