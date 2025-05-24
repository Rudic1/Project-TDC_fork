using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using TDC.Models;
using TDC.IService;

namespace TDC.Services
{
    public class FriendService : IFriendService
    {
        private readonly HttpClient _httpClient = new();

        public async Task<List<Friend>> GetFriendsForUser(string username)
        {
            var url = ConnectionUrls.development + $"/api/Friends/getFriends/{username}";
            var friends = await _httpClient.GetFromJsonAsync<List<Friend>>(url);
            return friends ?? new List<Friend>();
        }
    }
}
