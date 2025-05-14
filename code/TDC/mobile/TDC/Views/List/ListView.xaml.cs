using TDC.Models;
using TDC.Constants;
using TDC.Services;
using TDC.IService;
using TDC.Models.DTOs;


#if ANDROID
using Android.Views;
#endif



namespace TDC;

[QueryProperty(nameof(ListId), "id")]
public partial class ListView : IOnPageKeyDown
{
    private readonly IListService _listService;
    private readonly IListItemService _listItemService;
    private readonly UserService _userService;
    
    public long ListId { get; set; }
    public ToDoList List { get; set; }
    public List<ListItem> ExistingItems { get; set; }
    public List<long> DeletedItems = new List<long>();
    public List<ListItem> NewItems { get; set; }

    #region constructors
    public ListView(IListService listService, IListItemService listitemService, UserService userService)
    {
        InitializeComponent();
        _userService = userService;
        _listService = listService;
        _listItemService = listitemService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = SetUpAsync();
    }

    #endregion

    #region listeners
    private void OnNewItemClicked(object sender, EventArgs e)
    {
        var item = new ListItem("", 5);
        AddItemToView(item);
    }

    private void TitleEntryChanged(object sender, EventArgs e)
    {
        List.Name = this.FindByName<Entry>("TitleEntry").Text;
        if (!string.IsNullOrEmpty(this.FindByName<Entry>("TitleEntry").Text))
        {
            this.FindByName<Label>("ErrorLabel").IsVisible = false;
        }
    }

    private void OnEffortUpdated(object sender, EventArgs e)
    {
        this.FindByName<Label>("PointsLabel").Text = GetListPoints().ToString();
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
            UpdateExistingList();
            await Shell.Current.GoToAsync("///MainPage");
            return;
        }

        CreateNewList();
        await Shell.Current.GoToAsync("///MainPage");
    }

    private async void OnDeleteListClicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Delete list", "Would you like to delete this list?\nThis action can't be undone.", "Yes", "No");
        if (!answer) return;

        var currentUser = _userService.CurrentUser!.Username;
        await _listService.DeleteList(ListId, currentUser);
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
    private async void UpdateExistingList() {

        var currentUser = _userService.CurrentUser!.Username;
        foreach (var existingItem in ExistingItems)
        {
            await _listItemService.UpdateItemDescription(existingItem.ItemId, existingItem.Description);
            await _listItemService.UpdateItemEffort(existingItem.ItemId, existingItem.Effort);
            await _listItemService.SetItemStatus(existingItem.ItemId, currentUser, existingItem.IsDone);
        }

        foreach (var item in NewItems) {
            var itemDto = new ListItemSavingDto(item.Description, item.Effort);
            var itemId = await _listItemService.AddItemToList(ListId, itemDto);
            await _listItemService.SetItemStatus(itemId, currentUser, item.IsDone);
        }

        foreach (var id in DeletedItems)
        {
            await _listItemService.DeleteItem(id);
        }
    }

    private async void CreateNewList()
    {
        var currentUser = _userService.CurrentUser!.Username;
        var newId = await _listService.CreateList(List.Name, List.IsCollaborative, currentUser); //TODO: Add toggle option for collabs

        foreach (var item in NewItems)
        {
            var itemDto = new ListItemSavingDto(item.Description, item.Effort);
            var itemId = await _listItemService.AddItemToList(newId, itemDto);
            await _listItemService.SetItemStatus(itemId, currentUser, item.IsDone);
        }
    }

    private async Task SetUpAsync()
    {
        if (HasListId(ListId))
        {
            var currentUser = _userService.CurrentUser!.Username;
            List = await _listService.GetListById(ListId)!;
            ExistingItems = await _listItemService.GetItemsForList(ListId, currentUser);
            this.FindByName<Entry>("TitleEntry").Text = List.Name;
            AddItemsForExistingList();

            this.FindByName<Label>("PointsLabel").Text = GetListPoints().ToString();
            return;
        }
        List = new ToDoList();
        ExistingItems = new List<ListItem>();
    }

    private void AddItemsForExistingList()
    {
        foreach (var item in ExistingItems) {
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
        DeletedItems.Add(view.GetItem().ItemId);
        ExistingItems.Remove(view.GetItem());
        NewItems.Remove(view.GetItem());
        ItemsContainer.Children.Remove(view);
    }

    private static bool HasListId(long listId)
    {
        return listId != -1;
    }

    private int GetListPoints()
    {
        return ExistingItems.Sum(listItem => listItem.Effort * 5) + NewItems.Sum(listItem => listItem.Effort * 5);
    }

    private static bool HasInvalidTitleCharacters(string title)
    {
        return InvalidCharacters.InvalidTitle.Any(title.Contains);
    }
    #endregion
}