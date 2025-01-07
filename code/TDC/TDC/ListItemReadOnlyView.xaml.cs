using TDC.Models;

namespace TDC;

public partial class ListItemReadOnlyView : ContentView
{
    private readonly ListItem item;
    #region constructors
    public ListItemReadOnlyView(ListItem item)
	{
        this.item = item;
        InitializeComponent();
        this.FindByName<Label>("TaskLabel").Text = item.GetDescription();
        this.FindByName<CheckBox>("TaskCheckBox").IsChecked = item.IsDone();
        this.FindByName<Label>("PointsLabel").Text = (item.GetEffort() * 5).ToString();  //item.GetEffort() - 1;
    }
	#endregion
}