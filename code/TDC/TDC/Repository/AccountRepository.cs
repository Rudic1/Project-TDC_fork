using TDC.IRepository;
using TDC.Models;
using System.Linq; // Hinzugefügt für FirstOrDefault
using System.Diagnostics; // <-- Hinzufügen für Debug.WriteLine

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

    // NEU: Implementierung der Authentifizierungsmethode
    public Account? AuthenticateUser(string username, string password)
    {
        // ----- DEBUGGING START -----
        Debug.WriteLine($"[Auth] Versuch für Username: '{username}'");

        // !!! SICHERHEITSRISIKO: Nur für lokales Debugging, danach SOFORT entfernen !!!
        // Debug.WriteLine($"[Auth] Empfangenes Passwort: '{password}'");
        // Alternativ: Nur die Länge oder ob es nicht leer ist, loggen:
        Debug.WriteLine($"[Auth] Passwortlänge: {password}");
        // ----- DEBUGGING ENDE -----


        // Hole die Dummy-Daten (oder später echte Daten)
        List<Account> accounts = GetDummyAccounts();
        Debug.WriteLine($"accounts: {accounts.ToString}");
        // Finde den Benutzer nach Name und Passwort
        // WICHTIG: Passwörter sollten NIE im Klartext verglichen werden. Hier nur für die Dummy-Daten!
        return accounts.FirstOrDefault(acc =>
        {
            Debug.WriteLine($"{acc.Password} + {acc.Username}");
            return acc.Username.Equals(username, StringComparison.OrdinalIgnoreCase) // Case-Insensitive Vergleich
                   && acc.Password == password; // Unsicherer Klartext-Vergleich!
        });
        // Unsicherer Klartext-Vergleich!
    }
    #endregion

    #region privates

    // TO-DO: remove once database is implemented, only for android testing
    // Habe die Methode wieder auf 'private static' gesetzt, da sie nur intern gebraucht wird.
    private static List<Account> GetDummyAccounts()
    {
        var dummyAccounts = new List<Account>();
        // Korrigierte Account-Erstellung mit new List<long>() statt []
        var acc1 = new Account(1, "acc1", "description1", "mail 1", "pw1", new Character("", "", 0, new CharacterStats(0, 0, 0)), new List<long>(), new List<long>());
        dummyAccounts.Add(acc1);

        var acc2 = new Account(2, "acc2", "description2", "mail 2", "pw2", new Character("", "", 0, new CharacterStats(0, 0, 0)), new List<long> { 1 }, new List<long>());
        dummyAccounts.Add(acc2);

        var acc3 = new Account(3, "acc3", "description3", "mail 3", "pw3", new Character("", "", 0, new CharacterStats(0, 0, 0)), new List<long> { 2 }, new List<long> { 1 });
        dummyAccounts.Add(acc3);
        return dummyAccounts;
    }
    #endregion
}