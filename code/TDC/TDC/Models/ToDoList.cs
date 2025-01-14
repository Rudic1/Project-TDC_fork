using System.Diagnostics;

namespace TDC.Models;
public class ToDoList
{
    private readonly List<ListItem> items;
    private readonly List<Profile> members;
    private string name;
    private readonly string id; // TO-DO: add logic for data base, suggestion: <base-id>-<member-id>

    #region constructors 
    public ToDoList(string name)
    {
        id = Guid.NewGuid().ToString();
        items = new List<ListItem>();
        members = new List<Profile>();
        this.name = name;
    }

    public ToDoList(string name, string listId)
    {
        id = listId;
        items = new List<ListItem>();
        members = new List<Profile>();
        this.name = name;
    }
    #endregion

    #region getters & setters
    public void AddItem(ListItem item)
    {
        items.Add(item);
    }

    public void RemoveItem(ListItem item)
    {
        items.Remove(item);
    }

    public List<ListItem> GetItems()
    {
        return items;
    }

    public void AddMember(Profile member)
    {
        members.Add(member);
    }

    public void RemoveMember(Profile member)
    {
        members.Remove(member);
    }

    public List<Profile> GetMembers()
    {
        return members;
    }

    public void SetName(string n)
    {
        this.name = n;
    }

    public string GetName()
    {
        return name;
    }

    public string GetId()
    {
        Debug.WriteLine(id);
        return id;
    }
    #endregion

    public void DeleteList()
    {

    }

    public void FinishList()
    {
        
    }
}
