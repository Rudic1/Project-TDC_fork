namespace TDC.Services
{
    using TDC.Models;
    public class UserService
    {
        public Account? CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser != null;

        public void Login(string username, string password)
        {

        }

        public void Logout() {
            CurrentUser = null;
        }
    }
}
