using TDC.Models;

namespace TDC.IService
{
    public interface IFriendService
    {
        Task<List<string>> GetFriendsForUser(string username);
        Task<List<string>> GetFriendRequestsForUser(string username);
        Task SendFriendRequest(string sender, string receiver);
        Task AcceptFriendRequest(string username, string requestName);
        Task DenyFriendRequest(string username, string requestName);
    }
}
