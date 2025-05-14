using TDC.Models;

namespace TDC.IRepository;
public interface IListService
{
    public Task CreateList(string name, bool isCollaborative, string creator);
    public Task UpdateListTitle(string newTitle, long listId);
    public Task DeleteList(long listId, string sender);
    public Task FinishList(long listId, string sender);
    public ToDoList? GetListById(long listId);
    public List<ToDoList> GetAllListsForUser(string username);
}
