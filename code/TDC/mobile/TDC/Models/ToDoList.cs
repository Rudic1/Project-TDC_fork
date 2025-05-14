namespace TDC.Models;
public class ToDoList
{
    private readonly List<ListItem> Items;
    private readonly List<Profile> Members;
    public string Name { get; set; }
    public long ListID { get; } 
    public long UserId { get; }

    #region constructors 
    public ToDoList(string name, long userId)
    {
        ListID = 0;
        UserId = userId;
        Items = new List<ListItem>();
        Members = new List<Profile>();
        Name = name;
    }

    public ToDoList(long listId, string name, long userId)
    {
        ListID = listId;
        UserId = userId;
        Items = [];
        Members = [];
        Name = name;
    }
    #endregion

    #region getters & setters
    public void AddItem(ListItem item)
    {
        Items.Add(item);
    }

    public void RemoveItem(ListItem item)
    {
        Items.Remove(item);
    }

    public List<ListItem> GetItems()
    {
        return Items;
    }

    public void AddMember(Profile member)
    {
        Members.Add(member);
    }

    public void RemoveMember(Profile member)
    {
        Members.Remove(member);
    }
    #endregion
}
