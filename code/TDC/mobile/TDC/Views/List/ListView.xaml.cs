using TDC.Models;
using TDC.Constants;
using TDC.Repositories;



#if ANDROID
using Android.Views;
#endif

namespace TDC;

[QueryProperty(nameof(ListId), "id")]
public partial class ListView : IOnPageKeyDown
{
    private ToDoList list;
    private readonly ListRepository listRepository;
    public string? ListId { get; set; }

    #region constructors
    public ListView()
	{
        InitializeComponent();
        listRepository = new ListRepository();
        list = new ToDoList("", 0); //TO-DO: Replace 0 once login logic is implemented
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (HasListId(ListId))
        {
            list = listRepository.GetListById(ListId!, 0)!;
            this.FindByName<Entry>("TitleEntry").Text = list.Name;
            AddItemsForExistingList();
        }

        this.FindByName<Label>("PointsLabel").Text = GetListPoints(list).ToString();
    }
    #endregion

    #region listeners
    private void OnNewItemClicked(object sender, EventArgs e)
    {
        var item = new ListItem("", [], 5);
        list.AddItem(item);
        AddItemToView(item);
    }

    private void TitleEntryChanged(object sender, EventArgs e)
    {
        list.Name = this.FindByName<Entry>("TitleEntry").Text;
        if (!string.IsNullOrEmpty(this.FindByName<Entry>("TitleEntry").Text))
        {
            this.FindByName<Label>("ErrorLabel").IsVisible = false;
        }
    }

    private void OnEffortUpdated(object sender, EventArgs e)
    {
        this.FindByName<Label>("PointsLabel").Text = GetListPoints(list).ToString();
    }

    private async void OnSaveListClicked(object sender, EventArgs e)
    {
        var listName = TitleEntry.Text.Trim();
        if (string.IsNullOrWhiteSpace(listName) || HasInvalidTitleCharacters(listName))
        {
            this.FindByName<Label>("ErrorLabel").IsVisible = true;
            return;
        }

        if (HasListId(ListId))
        {
            listRepository.UpdateList(list, ListId!, 0);
            await Shell.Current.GoToAsync("///MainPage");
            return;
        }

        listRepository.CreateList(list);
        await Shell.Current.GoToAsync("///MainPage");
    }

    private async void OnDeleteListClicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Delete list", "Would you like to delete this list?\nThis action can't be undone.", "Yes", "No");
        if (!answer) return;
        listRepository.DeleteList(list.ListID, list.UserId);
        await Shell.Current.GoToAsync("///MainPage");
    }

    private void BackspaceEmitted()
    {
        foreach (var item in ItemsContainer.Children) {
            var view = (ListItemView)item;
            
            if(view.FindByName<Entry>("TaskEntry").IsFocused)
            {
                RemoveItem(view);
                return;
            }
        }
    }

    #if ANDROID
    public bool OnPageKeyDown(Keycode keyCode, KeyEvent e) {
        if(keyCode == Keycode.Del) {
            BackspaceEmitted();
            return true;
        }
        return false;
    }
    #endif
    #endregion

    #region privates
    private void AddItemsForExistingList()
    {
        foreach (var item in list.GetItems()) {
            AddItemToView(item);
        }
    }

    private void AddItemToView(ListItem item) {
        var listItemView = new ListItemView(item);
        ItemsContainer.Children.Add(listItemView);
        listItemView.NewItemOnEnter += OnNewItemClicked!;
        listItemView.EffortChanged += OnEffortUpdated!;
        listItemView.IsInitialized = true;
        OnEffortUpdated(this, EventArgs.Empty);
    }

    private void RemoveItem(ListItemView view)
    {
        list.RemoveItem(view.GetItem());
        ItemsContainer.Children.Remove(view);
    }

    private static bool HasListId(string? listId)
    {
        return !string.IsNullOrEmpty(listId);
    }

    private static int GetListPoints(ToDoList list)
    {
        return list.GetItems().Sum(listItem => listItem.GetEffort() * 5);
    }

    private static bool HasInvalidTitleCharacters(string title)
    {
        return InvalidCharacters.InvalidTitle.Any(title.Contains);
    }
    #endregion
}