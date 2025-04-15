namespace TDC.Services
{
    using TDC.Models;
    public class UserService
    {
        // CurrentUser hat bereits einen public setter, das ist gut!
        public Account? CurrentUser { get; set; }
        public bool IsLoggedIn => CurrentUser != null;

        // Geänderte Login-Methode: Akzeptiert das Account-Objekt
        public void Login(Account user)
        {
            // Setzt den aktuellen Benutzer
            this.CurrentUser = user;

            // Hier könntest du später weitere Logik hinzufügen,
            // z.B. Laden von Benutzereinstellungen, Auslösen eines Events, etc.
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}