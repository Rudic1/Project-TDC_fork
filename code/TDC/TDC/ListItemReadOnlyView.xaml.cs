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
        this.FindByName<Entry>("TaskEntry").Text = item.GetDescription();
        this.FindByName<CheckBox>("TaskCheckBox").IsChecked = item.IsDone();
        //this.FindByName<Picker>("TaskPicker").SelectedIndex = item.GetEffort() - 1;
	}
	#endregion
}