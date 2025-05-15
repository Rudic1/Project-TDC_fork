using TDC.Models;

namespace TDC;

public partial class ListItemReadOnlyView
{
    #region constructors
    public ListItemReadOnlyView(ListItem item)
	{
        InitializeComponent();
        this.FindByName<Label>("TaskLabel").Text = item.Description;
        this.FindByName<CheckBox>("TaskCheckBox").IsChecked = item.IsDone;
        this.FindByName<Label>("PointsLabel").Text = (item.Effort * 5).ToString();
    }
	#endregion
}