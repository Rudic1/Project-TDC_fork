using TDC.Models;
namespace TDC;

public partial class ListReadOnlyView
{
	public ListReadOnlyView(ToDoList list, List<ListItem> items)
    {
        InitializeComponent();
        this.FindByName<Label>("TitleLabel").Text = list.Name;
        this.FindByName<Label>("PointsLabel").Text = GetCompletedPoints(items).ToString();
        this.FindByName<Label>("AllPointsLabel").Text = GetTotalPoints(items).ToString();
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

    public int GetCompletedPoints(List<ListItem> items)
    {
        return items
            .Where(item => item.IsDone)
            .Sum(item => item.Effort * 5);
    }

    public int GetTotalPoints(List<ListItem> items)
    {
        return items.Sum(item => item.Effort * 5);
    }
    #endregion
}