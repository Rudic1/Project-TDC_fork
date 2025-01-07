using TDC.Models;

namespace TDC
{
    public partial class MainPage : ContentPage
    {
        private readonly ListRepository listRepository;
        private List<ToDoList> availableLists;
        private int shownListIndex;
        private bool listLoaded;
        #region constructors
        public MainPage()
        {
            shownListIndex = 0;
            listLoaded = false;
            availableLists = new List<ToDoList>();
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

        private void OnListPreviewTapped(object sender, EventArgs e)
        {
            Console.WriteLine("clicked preview");
            //await Shell.Current.GoToAsync("///ToDoListPage");
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            shownListIndex++;
            if (shownListIndex >= availableLists.Count)
            {
                shownListIndex = 0;
            }
            UpdateShownList();
        }

        private void OnPrevClicked(object sender, EventArgs e)
        {
            shownListIndex--;
            if (shownListIndex < 0)
            {
                shownListIndex = availableLists.Count - 1;
            }
            UpdateShownList();
        }

        #endregion

        #region privates
        private void LoadAvailableLists()
        {
            //TO-DO: Init via user
            this.availableLists = listRepository.GetLists();
            listLoaded = true;
            UpdateShownList();
        }

        private void UpdateShownList()
        {
            ListPreview.Children.Clear();
            if (availableLists.Count == 0)
            {
                var emptyListEntry = new Label
                {
                    Text = "No Lists available"
                };
                ListPreview.Children.Add(emptyListEntry);
                return;
            }
            var list = availableLists[shownListIndex];
            var listView = new ListReadOnlyView(list);
            ListPreview.Children.Add(listView);
        }
        #endregion
    }

}