namespace TDC.Services
{
    using TDC.Models;
    public class UserService
    {
        public Account? CurrentUser { get; set; }
        public bool IsLoggedIn => CurrentUser != null;

        public void Login(Account user)
        {
            this.CurrentUser = user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}