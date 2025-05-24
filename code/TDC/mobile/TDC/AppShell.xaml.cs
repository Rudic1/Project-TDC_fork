using TDC.Views.Login;
using TDC.Views.Profile;
using TDC.Views.FriendList;

namespace TDC
{
    public partial class AppShell : Shell
    {
        private readonly Services.UserService _userService;
        public AppShell()
        {
            InitializeComponent();

            _userService = App.Services.GetRequiredService<Services.UserService>();

            Routing.RegisterRoute("ToDoListPage", typeof(ListView));
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("FriendListPage", typeof(FriendListPage));
        }
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            _userService.Logout();
            //await Shell.Current.GoToAsync(nameof(LoginPage));
            Application.Current.MainPage = new LoginShell();
        }

        private async void ProfileIcon_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ProfilePage");
        }
    }

}
