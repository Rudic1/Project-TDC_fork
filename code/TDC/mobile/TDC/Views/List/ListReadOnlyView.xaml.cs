using TDC.Models;
namespace TDC;

public partial class ListReadOnlyView
{
	public ListReadOnlyView(ToDoList list, List<ListItem> items)
    {
        InitializeComponent();
        this.FindByName<Label>("TitleLabel").Text = list.Name;
        this.FindByName<Label>("PointsLabel").Text = GetListPoints(items).ToString();
        InitListItems(items);
	}

    #region privates

    private void InitListItems(List<ListItem> items)
    {
        foreach (var listItemView in items.Select(listItem => new ListItemReadOnlyView(listItem)
                 {
                     MaximumHeightRequest = 42,
                 }))
        {
            ItemsContainer.Children.Add(listItemView);
        }
    }

    private static int GetListPoints(List<ListItem> items)
    {
        return items.Sum(listItem => listItem.Effort*5);
    }
    #endregion
}