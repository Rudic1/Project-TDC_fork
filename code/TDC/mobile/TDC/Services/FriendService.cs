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

        public async Task<List<string>> GetFriendsForUser(string username)
        {
            var url = ConnectionUrls.development + $"/api/Account/getFriendsForUser/{username}";
            var friends = await _httpClient.GetFromJsonAsync<List<string>>(url);
            return friends ?? new List<string>();
        }
    }
}
