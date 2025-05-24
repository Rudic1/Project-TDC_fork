using TDC.Models;

namespace TDC.IService
{
    public interface IFriendService
    {
        Task<List<string>> GetFriendsForUser(string username);
    }
}
