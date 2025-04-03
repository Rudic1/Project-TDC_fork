using TDC.Models;
using TDC.Repositories;

namespace TDC
{
    public partial class MainPage
    {
        private ListRepository listRepository;
        private List<ToDoList> availableLists;
        private int shownListIndex;

        #region constructors
        public MainPage()
        {
            shownListIndex = 0;
            availableLists = new List<ToDoList>();
            listRepository = new ListRepository(); //TO-DO: init with user later
            InitializeComponent();
            LoadAvailableLists();
        }
        #endregion

        #region navigation
        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            shownListIndex = 0;
            availableLists = [];
            LoadAvailableLists();
        }

        private async void OnNewListClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"ToDoListPage");
        }

        private async void OnListPreviewTapped(object sender, EventArgs e)
        {
            if (availableLists.Count == 0)
            {
                return;
            }
            var id = availableLists[shownListIndex].ListID;
            await Shell.Current.GoToAsync($"ToDoListPage?id={id}");
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
            listRepository = new ListRepository();
            availableLists = listRepository.GetLists();
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