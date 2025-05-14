using System.Collections.Generic;
using TDC.IRepository;
using TDC.Models;

namespace TDC.Repositories
{
    public class ListRepository : IListRepository
    {
        #region publics
        public string CreateList(ToDoList list)
        {
            throw new NotImplementedException();
        }

        public void UpdateList(ToDoList newList, string listId, long userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteList(string listId, long userId)
        {
            throw new NotImplementedException();
        }

        public ToDoList? GetListById(string listId, long userId)
        {
            throw new NotImplementedException();
        }

        public List<ToDoList> GetAllListsForUser(long userId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
