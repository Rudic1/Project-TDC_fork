using TDC.Models;

namespace TDC;

public partial class ListReadOnlyView : ContentView
{
    private readonly ToDoList list;
	public ListReadOnlyView(ToDoList list)
    {
        this.list = list; 
        InitializeComponent();
        this.FindByName<Entry>("TitleEntry").Text = list.GetName();
        InitListItems();
	}

    #region privates

    private void InitListItems()
    {
        foreach (var listItem in list.GetItems())
        {
            var listItemView = new ListItemReadOnlyView(listItem)
            {
                MaximumHeightRequest = 42,
            };
            ItemsContainer.Children.Add(listItemView);
        }
    }
    #endregion
}