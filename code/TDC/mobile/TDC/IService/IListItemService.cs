using TDC.Models;
using TDC.Models.DTOs;

namespace TDC.IService
{
    public interface IListItemService
    {
        public Task<long> AddItemToList(long listId, ListItemSavingDto item);
        public Task DeleteItem(long itemId);
        public Task UpdateItemDescription(long itemId, string description);
        public Task UpdateItemEffort(long itemId, int effort);
        public Task SetItemStatus(long itemId, string updateFor, bool isDone);
        public Task<List<ListItem>> GetItemsForList(long listId, string currentUser);
    }
}
