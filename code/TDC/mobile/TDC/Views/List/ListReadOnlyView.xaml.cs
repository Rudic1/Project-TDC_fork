using TDC.Models;
namespace TDC;

public partial class ListReadOnlyView
{
	public ListReadOnlyView(ToDoList list)
    {
        InitializeComponent();
        this.FindByName<Label>("TitleLabel").Text = list.Name;
        this.FindByName<Label>("PointsLabel").Text = list.GetCompletedPoints().ToString();
        this.FindByName<Label>("AllPointsLabel").Text = list.GetTotalPoints().ToString();
        InitListItems(list);
	}

    #region privates

    private void InitListItems(ToDoList list)
    {
        foreach (var listItemView in list.GetItems().Select(listItem => new ListItemReadOnlyView(listItem)
                 {
                     MaximumHeightRequest = 42,
                 }))
        {
            ItemsContainer.Children.Add(listItemView);
        }
    }
    #endregion
}