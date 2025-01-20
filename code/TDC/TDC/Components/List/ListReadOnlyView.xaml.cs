using TDC.Models;

namespace TDC;

public partial class ListReadOnlyView : ContentView
{
    private readonly ToDoList list;
	public ListReadOnlyView(ToDoList list)
    {
        this.list = list; 
        InitializeComponent();
        this.FindByName<Label>("TitleLabel").Text = list.GetName();
        this.FindByName<Label>("PointsLabel").Text = GetListPoints(list).ToString();
        InitListItems(list);
	}

    #region privates

    private void InitListItems(ToDoList toDoList)
    {
        foreach (var listItem in toDoList.GetItems())
        {
            var listItemView = new ListItemReadOnlyView(listItem)
            {
                MaximumHeightRequest = 42,
            };
            ItemsContainer.Children.Add(listItemView);
        }
    }

    private int GetListPoints(ToDoList toDoList)
    {
        return toDoList.GetItems().Sum(listItem => listItem.GetEffort()*5);
    }
    #endregion
}