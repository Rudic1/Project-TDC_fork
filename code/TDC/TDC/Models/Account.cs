namespace TDC.Models;
public class Account: Profile
{
    private readonly List<string> friends; 
    private readonly List<string> requests;
    private readonly List<string> lists;
    private string email;
    private string password;

    #region constructors
    // new account case
    public Account(string id, string name, string description, string email, string password, Character character, int level)
    : base(id, name, character, description, level)
    {
        this.friends = [];
        this.requests = [];
        this.lists = [];
        this.email = email;
        this.password = password;
    }

    // existing account -> identify via repository
    public Account(string id, string name, string description, string email, string password, Character character, int level, List<string> friends, List<string> requests, List<string> lists)
        : base(id, name, character, description, level)
    {
        this.friends = friends;
        this.requests = requests;
        this.lists = lists;
        this.email = email;
        this.password = password;
        this.Level = level;
    }

    #endregion

    #region getters & setters
    public void SetName(string name) { 
        this.Name = name;
    }

    public void SetCharacter(Character character) { 
        this.Character = character;
    }

    public void SetDescription(string description) { 
        this.Description = description;
    }

    public void SetPassword(string pw)
    {
        this.password = pw;
    }

    public string GetPassword()
    {
        return password;
    }

    public void SetEmail(string mail)
    {
        this.email = mail;
    }

    public string GetEmail()
    {
        return email;
    }

    public void AddFriend(string friendId)
    {
        friends.Add(friendId);
    }

    public void RemoveFriend(string friendId)
    {
        friends.Remove(friendId);
    }

    public List<string> GetFriendList()
    {
        return friends;
    }

    public void AddList(string listId)
    {
        lists.Add(listId);
    }

    public void RemoveList(string listId)
    {
        lists.Remove(listId);
    }

    public List<string> GetLists()
    {
        return lists;
    }


    public List<string> GetRequests() { 
        return requests;
    }

    public void SendRequest(string profileId) {
        requests.Add(profileId);
    }
    #endregion
}

