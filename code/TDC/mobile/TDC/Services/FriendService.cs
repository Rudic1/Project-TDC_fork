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

        public async Task<List<string>> GetFriendRequestsForUser(string username)
        {
            var url = ConnectionUrls.development + $"/api/Account/getFriendRequestsForUser/{username}";
            var requests = await _httpClient.GetFromJsonAsync<List<string>>(url);
            return requests ?? new List<string>();
        }

        public async Task SendFriendRequest(string sender, string receiver)
        {
            var url = ConnectionUrls.development + $"/api/Account/sendFriendRequest/{sender}/{receiver}";
            var response = await _httpClient.PutAsync(url, null);
            response.EnsureSuccessStatusCode();
        }

        public async Task AcceptFriendRequest(string username, string requestName)
        {
            var url = ConnectionUrls.development + $"/api/Account/acceptFriendRequest/{username}/{requestName}";
            var response = await _httpClient.PostAsync(url, null);
            response.EnsureSuccessStatusCode();
        }

        public async Task DenyFriendRequest(string username, string requestName)
        {
            var url = ConnectionUrls.development + $"/api/Account/denyFriendRequest/{username}/{requestName}";
            var response = await _httpClient.PostAsync(url, null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> AccountExists(string username)
        {
            var url = ConnectionUrls.development + $"/api/Account/accountExists/{username}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return false;

            var content = await response.Content.ReadAsStringAsync();
            return bool.TryParse(content, out var exists) && exists;
        }

        public async Task<List<string>> GetSentFriendRequestsForUser(string username)
        {
            var url = ConnectionUrls.development + $"/api/Account/getSentFriendRequestsForUser/{username}";
            var requests = await _httpClient.GetFromJsonAsync<List<string>>(url);
            return requests ?? new List<string>();
        }

        public async Task CancelFriendRequest(string sender, string receiver)
        {
            var url = ConnectionUrls.development + $"/api/Account/cancelFriendRequest/{sender}/{receiver}";
            var response = await _httpClient.PutAsync(url, null);
            response.EnsureSuccessStatusCode();
        }
    }

}