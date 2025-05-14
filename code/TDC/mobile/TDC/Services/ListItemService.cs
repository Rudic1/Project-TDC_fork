using System.Text;
using System.Text.Json;
using TDC.IService;
using TDC.Models;
using TDC.Models.DTOs;

namespace TDC.Services
{
    public class ListItemService : IListItemService
    {
        private readonly HttpClient httpClient = new();
        public async Task AddItemToList(long listId, ListItemSavingDto item)
        {
            var url = ConnectionUrls.development + $"/api/List/addItemToList/{listId}";

            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PutAsync(url, content);
        }

        public async Task DeleteItem(long itemId)
        {
            var url = ConnectionUrls.development + $"/api/List/deleteItem/{itemId}";
            var data = new { };
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PostAsync(url, content);
        }

        public async Task<List<ListItem>> GetItemsForList(long listId, string currentUser)
        {
            var url = ConnectionUrls.development + $"/api/List/getItemsForList/{listId}/{currentUser}";

            var response = await httpClient.GetAsync(url);
            string responseContent = await response.Content.ReadAsStringAsync();
            var itemDtos = JsonSerializer.Deserialize<List<ListItemDto>>(responseContent)!;
            var items = new List<ListItem>();
            foreach (var itemDto in itemDtos) {
                items.Add(new ListItem(itemDto.ItemId, itemDto.Description, itemDto.IsDone, itemDto.FinishedMembers, itemDto.Effort));
            }
            return items;
        }

        public async Task SetItemStatus(long itemId, string updateFor, bool isDone)
        {
            var url = ConnectionUrls.development + $"/api/List/setItemStatus/{itemId}";
            var data = new
            {
                UpdateForUser = updateFor,
                IsDone = isDone,
            };
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PostAsync(url, content);
        }

        public async Task UpdateItemDescription(long itemId, string description)
        {
            var url = ConnectionUrls.development + $"/api/List/updateItemDescription/{itemId}";
            var data = new
            {
                description = description
            };
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PostAsync(url, content);
        }

        public async Task UpdateItemEffort(long itemId, int effort)
        {
            var url = ConnectionUrls.development + $"/api/List/updateItemEffort/{itemId}";
            var data = new {
                newEffort = effort
            };
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PostAsync(url, content);
        }
    }
}
