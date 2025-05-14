using System.Text;
using System.Text.Json;
using TDC.IRepository;
using TDC.Models;
using TDC.Models.DTOs;
using TDC.Repository;
using TDC.Services;

namespace TDC.Repositories
{
    public class ListRepository : IListRepository
    {
        private readonly HttpClient httpClient = new();
        private readonly UserService userService = App.Services.GetService<UserService>()!;


        #region publics
        public async Task CreateList(ToDoList list)
        {
            var dto = new ToDoListDto(0, list.Name, [], [], list.isCollaborative); //To-DO: Add toggle button for collaborative 
            var sender = userService.CurrentUser!.Username;
            var url = ConnectionUrls.development + $"/api/List/createList/{sender}";

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PutAsync(url, content);
        }

        public void UpdateList(ToDoList newList, long listId, string username)
        {
            throw new NotImplementedException();
        }

        public void DeleteList(long listId, string username)
        {
            throw new NotImplementedException();
        }

        public ToDoList? GetListById(long listId, string username)
        {
            throw new NotImplementedException();
        }

        public List<ToDoList> GetAllListsForUser(string username)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
