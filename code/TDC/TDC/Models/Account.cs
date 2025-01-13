using System.Collections.Generic;

namespace TDC.Models;
public class Account: Profile
{
    private readonly List<Profile> friends;
    private readonly List<Profile> requests;
    private readonly List<ToDoList> lists;
    private string email;
    private string password;

    #region constructors
    // new account case
    public Account(string id, string name, string description, string email, string password, Character character)
    : base(id, name, character, description, 0)
    {
        this.friends = [];
        this.requests = [];
        this.lists = [];
        this.email = email;
        this.password = password;
        this.Level = 0;
    }

    // existing account -> identify via repository
    public Account(string id, string name, string description, string email, string password, List<Profile> friends, List<Profile> requests, List<ToDoList> lists, Character character)
        : base(id, name, character, description, 0)
    {
        this.friends = friends;
        this.requests = requests;
        this.lists = lists;
        this.email = email;
        this.password = password;
        this.Level = 0;
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

    public void AddFriend(Profile friend)
    {
        friends.Add(friend);
    }

    public void RemoveFriend(Profile friend)
    {
        friends.Remove(friend);
    }

    public List<Profile> GetFriendList()
    {
        return friends;
    }

    public void AddList(ToDoList list)
    {
        lists.Add(list);
    }

    public void RemoveList(ToDoList list)
    {
        lists.Remove(list);
    }

    public List<ToDoList> GetLists()
    {
        return lists;
    }


    public List<Profile> GetRequests() { 
        return requests;
    }

    public void SendRequest(Profile profile) {
        
    }
    #endregion
}

