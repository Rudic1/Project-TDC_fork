using TDC.Models;

namespace TDC.IRepository;
public interface IListRepository
{
    public Task CreateList(string name, bool isCollaborative);
    public Task UpdateListTitle(string newTitle, long listId);
    public Task DeleteList(long listId, string username);
    public Task FinishList(long listId, string username);
    public ToDoList? GetListById(long listId);
    public List<ToDoList> GetAllListsForUser(string username);
}
