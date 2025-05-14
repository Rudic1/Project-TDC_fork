using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDC.IServices;
using TDC.Models.DTOs;

namespace TDC.Services
{
    public class ListItemService : IListItemService
    {
        private readonly HttpClient httpClient = new();
        public Task AddItemToList(long listId, ListItemSavingDto item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(long itemId)
        {
            throw new NotImplementedException();
        }

        public Task SetItemStatus(long itemId, bool isDone)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemDescription(long itemId, string description)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemEffort(long itemId, int effort)
        {
            throw new NotImplementedException();
        }
    }
}
