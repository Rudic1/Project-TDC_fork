using TDC.Models.DTOs;

namespace TDC.IServices
{
    public interface IListItemService
    {
        public Task AddItemToList(long listId, ListItemSavingDto item);
        public Task DeleteItem(long itemId);
        public Task UpdateItemDescription(long itemId, string description);
        public Task UpdateItemEffort(long itemId, int effort);
        public Task SetItemStatus(long itemId, bool isDone);
    }
}
