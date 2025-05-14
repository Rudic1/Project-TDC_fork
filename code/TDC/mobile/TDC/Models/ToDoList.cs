namespace TDC.Models;
public class ToDoList
{
    private readonly List<ListItem> Items;
    private readonly List<Profile> Members;
    public string Name { get; set; }
    public long ListID { get; } 
    public string Username { get; }
    public bool isCollaborative { get; set; }

    #region constructors 
    public ToDoList(string name, string username)
    {
        ListID = 0;
        Username = username;
        Items = [];
        Members = [];
        Name = name;
    }

    public ToDoList(long listId, string name, string username)
    {
        ListID = listId;
        Username = username;
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

    public List<Profile> GetMembers()
    {
        throw new NotImplementedException();
    }

#endregion
}
