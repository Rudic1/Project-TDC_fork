namespace TDC.Views.Profile;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}
    private void DescriptionEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        int remaining = 150 - e.NewTextValue?.Length ?? 0;
        CharacterCountLabel.Text = $"{remaining}";
    }
        private async void OpenFriendList_Clicked(object sender, EventArgs e)
    {
        // TODO: Navigation to FriendListPage
        //await Shell.Current.GoToAsync("FriendListPage");
    }
}