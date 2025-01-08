namespace TDC
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ToDoListPage", typeof(ListView));
        }
    }
}
