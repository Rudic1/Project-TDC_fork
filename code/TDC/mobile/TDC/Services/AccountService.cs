using System.Text.Json;
using System.Text;
using TDC.IRepository;
using TDC.Models;
using TDC.Models.DTOs;

namespace TDC.Services;

public class AccountService : IAccountService
{
    private readonly HttpClient httpClient = new();
    public async Task<bool> AuthenticateUserLogin(string username, string password)
    {
        var url = ConnectionUrls.development + $"/api/Account/logInWithUsername";
        var data = new
        {
            Username = username,
            Password = password
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        return bool.Parse(responseContent);
    }

    public async Task<bool> AuthenticateUserLoginWithEmail(string email, string password)
    {
        var url = ConnectionUrls.development + $"/api/Account/logInWithMail";
        var data = new
        {
            Email = email,
            Password = password
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        return bool.Parse(responseContent);
    }

    public async Task<bool> CreateAccount(Account account, string password)
    {
        var dto = new AccountDto(account.Username, account.Email, password, account.Description); 
       
        var url = ConnectionUrls.development + $"/api/Account/registerUser";

        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PutAsync(url, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        return bool.Parse(responseContent);
    }

    public Task<bool> DeleteAccount(string username)
    {
        throw new NotImplementedException(); //TODO: Add Backend-Endpoint
    }

    public Task<bool> EmailIsTaken(string email)
    {
        throw new NotImplementedException(); //TODO: Add BE-Endpoint
    }

    public async Task<Account> GetAccountByUsername(string username)
    {
        var url = ConnectionUrls.development + $"/api/Account/getAccountData/{username}";

        var response = await httpClient.GetAsync(url);

        string responseContent = await response.Content.ReadAsStringAsync();
        var accountDto = JsonSerializer.Deserialize<AccountLoadingDto>(responseContent)!;
        return new Account(username, accountDto.Description, accountDto.Email);
    }

    public async Task UpdateDescription(string description, string username)
    {
        var url = ConnectionUrls.development + $"/api/Account/updateUserDescription/{username}";
        var data = new
        {
            Description = description
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        await httpClient.PostAsync(url, content);
    }

    public async Task<bool> UpdateEmail(string email, string username)
    {
        var url = ConnectionUrls.development + $"/api/Account/updateEmail/{username}";
        var data = new
        {
            Email = email
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        return bool.Parse(responseContent);
    }

    public async Task<bool> UpdatePassword(string password, string username)
    {
        var url = ConnectionUrls.development + $"/api/Account/updatePassword/{username}";
        var data = new
        {
            Password = password
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        return bool.Parse(responseContent);
    }

    public async Task<bool> UpdateUsername(string newUsername, string oldUsername)
    {
        var url = ConnectionUrls.development + $"/api/Account/updateUsername/{oldUsername}/{newUsername}";
        var data = new { };
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        return bool.Parse(responseContent);
    }

    public Task<bool> UsernameIsTaken(string username)
    {
        throw new NotImplementedException(); //TODO: Add BE-Endpoint
    }
}