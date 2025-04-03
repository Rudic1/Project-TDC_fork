using System.Diagnostics;

namespace TDC.Models;
public class ToDoList
{
    private readonly List<ListItem> Items;
    private readonly List<Profile> Members;
    public string Name { get; set; }
    public string ListID { get; } 

    #region constructors 
    public ToDoList(string name)
    {
        ListID = Guid.NewGuid().ToString(); //TO-DO: Replace with long later and use database
        Items = new List<ListItem>();
        Members = new List<Profile>();
        Name = name;
    }

    public ToDoList(string name, string listId)
    {
        ListID = listId;
        Items = new List<ListItem>();
        Members = new List<Profile>();
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
