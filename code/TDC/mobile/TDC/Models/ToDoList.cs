namespace TDC.Models;
public class ToDoList
{
    public string Name { get; set; }
    public long ListID { get; } 
    public bool IsCollaborative { get; set; }

    #region constructors 
    public ToDoList(long listId, string name, bool isCollaborative)
    {
        ListID = listId;
        Name = name;
        IsCollaborative = isCollaborative;
    }

    public ToDoList() {
        ListID = 0;
        Name = "";
        IsCollaborative = true;
    }
    #endregion
}
