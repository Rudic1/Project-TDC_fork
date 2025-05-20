namespace TDC
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }
        public App(IServiceProvider services)
        {
            InitializeComponent();

            Services = services;

            var userService = services.GetRequiredService<Services.UserService>();

            if (userService.IsLoggedIn)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginShell();
            }
        }
    }
}
