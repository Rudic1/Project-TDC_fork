using TDC.Models;

namespace TDC.IService;
public interface IListService
{
    public Task<long> CreateList(string name, bool isCollaborative, string creator);
    public Task UpdateListTitle(string newTitle, long listId);
    public Task DeleteList(long listId, string sender);
    public Task FinishList(long listId, string sender);
    public Task<ToDoList> GetListById(long listId);
    public Task<List<ToDoList>> GetAllListsForUser(string username);
}
