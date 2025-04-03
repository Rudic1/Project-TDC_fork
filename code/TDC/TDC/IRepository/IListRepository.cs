using TDC.Models;

namespace TDC.IRepository;
public interface IListRepository
{
    public void CreateList(ToDoList list);
    public void UpdateList(ToDoList newList, string listId);
    public void DeleteList(string listId);
    public ToDoList GetListById(string listId, long userId);
    public List<ToDoList> GetAllListsForUser(long userId);
}
