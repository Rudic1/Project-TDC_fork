using TDC.Models;

#if ANDROID
using Android.Views;
#endif

namespace TDC;

public partial class ListView : ContentPage, IOnPageKeyDown
{
    private readonly ToDoList list;
    private readonly ListRepository listRepository;

    #region constructors
    public ListView()
	{
        InitializeComponent();
        listRepository = new ListRepository();
        list = new ToDoList("");
        this.FindByName<Label>("PointsLabel").Text = GetListPoints(list).ToString();
    }

    public ListView(ToDoList list)
    {
        InitializeComponent();
        this.list = list;
        listRepository = new ListRepository();
        this.FindByName<Entry>("TitleEntry").Text = list.GetName();
        //init items here
        this.FindByName<Label>("PointsLabel").Text = GetListPoints(list).ToString();
    }
    #endregion

    #region listeners
    private void OnNewItemClicked(object sender, EventArgs e)
    {
        list.AddItem(new ListItem("", [], 5));

        var listItemView = new ListItemView(list.GetItems().Last());
        ItemsContainer.Children.Add(listItemView);
        listItemView.NewItemOnEnter += OnNewItemClicked!;
        listItemView.EffortChanged += OnEffortUpdated!;
        listItemView.isInitialized = true;
        OnEffortUpdated(this, e);
    }

    private void OnEffortUpdated(object sender, EventArgs e)
    {
        this.FindByName<Label>("PointsLabel").Text = GetListPoints(list).ToString();
    }

    private async void OnSaveListClicked(object sender, EventArgs e)
    {
        // get name from input field
        var listName = TitleEntry.Text?.Trim();

        // if no name entered, ask user to put name
        if (string.IsNullOrWhiteSpace(listName))
        {
            var result = await DisplayPromptAsync("Enter List Name", "Please provide a name for the list: ");
            if (!string.IsNullOrWhiteSpace(result))
            {
                listName = result;
                TitleEntry.Text = result;
            }
        }
        // set name of list
        if (!string.IsNullOrWhiteSpace(listName))
        {
            list.SetName(listName);
        }
        // save list
        listRepository.AddList(list);
    }

    private void BackspaceEmitted()
    {
        foreach (var item in ItemsContainer.Children) {
            var view = (ListItemView)item;

            if(view.FindByName<Entry>("TaskEntry").IsFocused)
            {
                RemoveItem(view);
                break;
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

    private void RemoveItem(ListItemView view)
    {
        list.RemoveItem(view.GetItem());
        ItemsContainer.Children.Remove(view);
    }

    private int GetListPoints(ToDoList toDoList)
    {
        return toDoList.GetItems().Sum(listItem => listItem.GetEffort() * 5);
    }
    #endregion
}