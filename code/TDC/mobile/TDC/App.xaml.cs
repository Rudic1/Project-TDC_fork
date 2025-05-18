namespace TDC
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }
        public App(IServiceProvider services)
        {
            InitializeComponent();

            Services = services;

            MainPage = new AppShell();

            GoToLoginPage();
        }

        private async void GoToLoginPage()
        {
            await Task.Delay(100);

            await Shell.Current.GoToAsync("LoginPage");
        }
    }
}
