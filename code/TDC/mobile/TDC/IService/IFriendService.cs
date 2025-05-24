using TDC.Models;

namespace TDC.IService
{
    public interface IFriendService
    {
        Task<List<Friend>> GetFriendsForUser(string username);
    }
}
