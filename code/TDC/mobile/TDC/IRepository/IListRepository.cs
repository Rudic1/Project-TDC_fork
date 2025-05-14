using TDC.Models;

namespace TDC.IRepository;
public interface IListRepository
{
    public Task CreateList(ToDoList list);
    public void UpdateList(ToDoList newList, long listId, string username);
    public void DeleteList(long listId, string username);
    public ToDoList? GetListById(long listId, string username);
    public List<ToDoList> GetAllListsForUser(string username);
}
