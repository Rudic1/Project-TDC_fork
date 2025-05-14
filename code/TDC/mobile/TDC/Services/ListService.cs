using System.Text;
using System.Text.Json;
using TDC.IRepository;
using TDC.Models;
using TDC.Models.DTOs;

namespace TDC.Services
{
    public class ListService : IListService
    {
        private readonly HttpClient httpClient = new();
        private readonly UserService userService = App.Services.GetService<UserService>()!;

        #region publics
        public async Task CreateList(string name, bool isCollaborative)
        {
            var dto = new ToDoListDto(0, name, [], [], isCollaborative); //TODO: Add toggle button for collaborative 
            var sender = userService.CurrentUser!.Username;
            var url = ConnectionUrls.development + $"/api/List/createList/{sender}";

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PutAsync(url, content);
        }

        public async Task UpdateListTitle(string newTitle, long listId)
        {
            var url = ConnectionUrls.development + $"/api/List/updateListTitle/{listId}";
            var data = new
            {
                newTitle = newTitle,
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
        }

        public async Task DeleteList(long listId)
        {
            var url = ConnectionUrls.development + $"/api/List/deleteList/{listId}";
            var currentUser = userService.CurrentUser!.Username;
            var data = new
            {
                sender = currentUser,
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
        }

        public Task FinishList(long listId, string username)
        {
            throw new NotImplementedException();
        }

        public ToDoList? GetListById(long listId)
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
