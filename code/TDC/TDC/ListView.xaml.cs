using TDC.Models;
using System.Diagnostics;
using TDC.Constants;
/* Nicht gemergte Änderung aus Projekt "TDC (net8.0-windows10.0.19041.0)"
Hinzugefügt:
using TDC.Repositories;
*/
using TDC.Repositories;



#if ANDROID
using Android.Views;
#endif

namespace TDC;

[QueryProperty(nameof(listId), "id")]
public partial class ListView : ContentPage, IOnPageKeyDown
{
    private ToDoList list;
    private readonly ListRepository listRepository;
    public string? listId { get; set; }

    #region constructors
    public ListView()
	{
        InitializeComponent();
        listRepository = new ListRepository();
        list = new ToDoList("");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!string.IsNullOrEmpty(listId)) // existing list
        {
            list = listRepository.GetListFromID(listId)!;
            this.FindByName<Entry>("TitleEntry").Text = list.GetName();
            AddItemsForExistingList(list);
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
        list.SetName(this.FindByName<Entry>("TitleEntry").Text);
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
        var listName = TitleEntry.Text?.Trim();
        if (string.IsNullOrWhiteSpace(listName) || HasInvalidTitleCharacters(listName)) // if no name entered, ask user to put name
        {
            this.FindByName<Label>("ErrorLabel").IsVisible = true;
            return;
        }

        if (!string.IsNullOrEmpty(listId)) //existing list -> update attributes
        {
            listRepository.UpdateList(list, listId);
            await Shell.Current.GoToAsync("///MainPage");
            return;
        }

        // save list
        listRepository.AddList(list);
        await Shell.Current.GoToAsync("///MainPage");
    }

    private async void OnDeleteListClicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Delete list", "Would you like to delete this list?\nThis action can't be undone.", "Yes", "No");
        if (!answer) return;
        listRepository.RemoveList(list);
        await Shell.Current.GoToAsync("///MainPage");
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
    private void AddItemsForExistingList(ToDoList list)
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
        listItemView.isInitialized = true;
        OnEffortUpdated(this, EventArgs.Empty);
    }

    private void RemoveItem(ListItemView view)
    {
        list.RemoveItem(view.GetItem());
        ItemsContainer.Children.Remove(view);
    }

    private static int GetListPoints(ToDoList toDoList)
    {
        return toDoList.GetItems().Sum(listItem => listItem.GetEffort() * 5);
    }

    private static bool HasInvalidTitleCharacters(string title)
    {
        return InvalidCharacters.InvalidTitle.Any(title.Contains);
    }
    #endregion
}