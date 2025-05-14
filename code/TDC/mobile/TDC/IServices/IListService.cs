using TDC.Models;

namespace TDC.IRepository;
public interface IListService
{
    public Task CreateList(string name, bool isCollaborative);
    public Task UpdateListTitle(string newTitle, long listId);
    public Task DeleteList(long listId);
    public Task FinishList(long listId);
    public ToDoList? GetListById(long listId);
    public List<ToDoList> GetAllListsForUser(string username);
}
