using TDC.IRepository;
using TDC.Models;

namespace TDC.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.."));
    private readonly string filePath;

    #region constructors
    public AccountRepository()
    {
        filePath = Path.Combine(projectPath, "Data/Accounts");

#if ANDROID
            var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            filePath = Path.Combine(directoryPath, "Accounts");
#endif
    }
    #endregion

    #region publics
    public void CreateAccount(Account account)
    {
        throw new NotImplementedException(); // TODO: Implementieren
    }

    public void DeleteAccount(Account account)
    {
        throw new NotImplementedException(); // TODO: Implementieren
    }

    public bool EmailIsTaken(string email)
    {
        throw new NotImplementedException(); // TODO: Implementieren
    }

    public Account GetAccountById(long id)
    {
        throw new NotImplementedException(); // TODO: Implementieren
    }

    public void UpdateAccount(Account account)
    {
        throw new NotImplementedException(); // TODO: Implementieren
    }

    public bool UsernameIsTaken(string username)
    {
        throw new NotImplementedException(); // TODO: Implementieren
    }

    public Account? AuthenticateUserLogin(string username, string password)
    {
        throw new NotImplementedException();
    }
    #endregion
}