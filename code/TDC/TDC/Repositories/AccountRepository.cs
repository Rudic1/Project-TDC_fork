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
        filePath = Path.Combine(projectPath, "TestDB/Accounts");

        #if ANDROID
            var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            filePath = Path.Combine(directoryPath, "TestDB/Accounts");
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
        // testing only
        #if ANDROID
            return GetDummyAccounts();
        #endif
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
            accounts.Add(LoadAccountFromDirectory(dir + "/"));
        }
    }

    private Account LoadAccountFromDirectory(string accountPath)
    {
        var reader = new StreamReader(accountPath + "info.csv");
        var accInfo = reader.ReadToEnd().Split(Environment.NewLine)[1].Split(';');

        var acc = new Account(accInfo[0], accInfo[1], accInfo[2], accInfo[3], accInfo[4], new Character(), int.Parse(accInfo[6])); //TO-DO: Implement Character database
        SetListsForAccount(acc, accountPath + "lists.csv");
        SetFriendsForAccount(acc, accountPath + "friends.csv");
        SetRequestsForAccount(acc, accountPath + "requests.csv");
        return acc;
    }

    private static void SetListsForAccount(Account acc, string fullPath)
    {
        var reader = new StreamReader(fullPath);
        var listIds = reader.ReadToEnd().Split(Environment.NewLine);
        foreach (var id in listIds)
        {
            if (string.IsNullOrWhiteSpace(id)) { continue;}
            acc.AddList(id); //TO-DO: secure connected lists are always saved
        }
    }

    private static void SetFriendsForAccount(Account acc, string fullPath)
    {
        var reader = new StreamReader(fullPath);
        var friendIds = reader.ReadToEnd().Split(Environment.NewLine);
        foreach (var id in friendIds)
        {
            if (string.IsNullOrWhiteSpace(id)) { continue;}
            acc.AddFriend(id);
        }
    }

    private static void SetRequestsForAccount(Account acc, string fullPath)
    {
        var reader = new StreamReader(fullPath);
        var requestIds = reader.ReadToEnd().Split(Environment.NewLine);
        foreach (var id in requestIds)
        {
            if (string.IsNullOrWhiteSpace(id)) { continue;}
            acc.SendRequest(id);
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
        writer.WriteLine("AccountID;Name;Description;E-Mail;Password;CharacterID;Level");
        writer.WriteLine(acc.GetId() + ";" + acc.GetName() + ";" + acc.GetDescription() + ";" + acc.GetEmail() + ";" + acc.GetPassword() + ";" + "default" + ";" + acc.GetLevel()); //TO-DO: add character database or use different saving method
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

    // TO-DO: remove once database is implemented, only for android testing
    private static List<Account> GetDummyAccounts()
    {
        var dummyAccounts = new List<Account>();
        var acc1 = new Account("1", "acc1", "description1", "mail 1", "pw1", new Character(), 2, [], [], []);
        dummyAccounts.Add(acc1);

        var acc2 = new Account("2", "acc2", "description2", "mail 2", "pw2", new Character(), 0, ["1"], [], []);
        dummyAccounts.Add(acc2);

        var acc3 = new Account("3", "acc3", "description3", "mail 3", "pw3", new Character(), 5, ["2"], ["1"], []);
        dummyAccounts.Add(acc3);
        return dummyAccounts;
    }
    #endregion
}
