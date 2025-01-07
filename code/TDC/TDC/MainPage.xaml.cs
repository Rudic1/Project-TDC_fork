using TDC.Models;

namespace TDC
{
    public partial class MainPage : ContentPage
    {
        private ListRepository listRepository;
        #region constructors
        public MainPage()
        {
            listRepository = new ListRepository(); //init with user later
            InitializeComponent();
            LoadAvailableLists();
        }
        #endregion

        #region navigation
        private async void OnNewListClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///ToDoListPage");
        }
        #endregion

        #region privates
        private void LoadAvailableLists()
        {
            ListPreview.Children.Clear();
            //TO-DO: Init via user
            var availableLists = listRepository.GetLists();
            if (availableLists.Count == 0)
            {
                var emptyListEntry = new Entry
                {
                    Text = "No Lists available"
                };
                return;
            }

            //TO-DO: add navigation option to switch between lists
            var list = availableLists[0];
            var listView = new ListReadOnlyView(list);
            ListPreview.Children.Add(listView);
        }
        #endregion
    }

}