using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Domain
{
    public class AccountHandler(IAccountRepository accountRepository, IFriendRepository friendRepository, IFriendRequestRepository friendRequestRepository) : IAccountHandler
    {
        public readonly IAccountRepository _accountRepository = accountRepository;
        public readonly IFriendRepository _friendRepository = friendRepository;
        public readonly IFriendRequestRepository _friendRequestRepository = friendRequestRepository;

        public Task AcceptFriendRequest(string username, string request)
        {
            var friendsOfUser = _friendRepository.GetFriendsForUser(username);
            var friendsOfRequest = _friendRepository.GetFriendsForUser(request);

            if (!friendsOfUser.Contains(request)) {
                this._friendRepository.AddFriend(username, request);
            }

            if (!friendsOfRequest.Contains(username))
            {
                this._friendRepository.AddFriend(request, username);
            }

            this._friendRequestRepository.DeleteFriendRequest(username, request);
            this._friendRequestRepository.DeleteFriendRequest(request, username);
            
            return Task.CompletedTask;
        }

        public Task CancelFriendRequest(string username, string request)
        {
            this._friendRequestRepository.DeleteFriendRequest(username, request);
            return Task.CompletedTask;
        }

        public Task DenyFriendRequest(string username, string requestName)
        {
            this._friendRequestRepository.DeleteFriendRequest(username, requestName);
            return Task.CompletedTask;
        }

        public AccountLoadingDto GetAccountByUsername(string username)
        {
            var accountDbo = _accountRepository.GetAccountByUsername(username)!;
            return new AccountLoadingDto(accountDbo.Username, accountDbo.Email, accountDbo.Description);
        }

        public List<string> GetFriendsForUser(string username)
        {
            return _friendRepository.GetFriendsForUser(username);
        }

        public List<string> GetRequestsForUser(string username)
        {
            return _friendRequestRepository.GetRequestsForUser(username);
        }

        public bool LoginWithMail(string email, string password)
        {
            if (!AccountWithEmailExists(email)) { return false;}
            var accountDbo = _accountRepository.GetAccountByEmail(email)!;
            return accountDbo.Password.Equals(password);
        }

        public bool LoginWithUsername(string username, string password)
        {
            if (!AccountWithUsernameExists(username)) { return false;}
            var accountDbo = _accountRepository.GetAccountByUsername(username)!;
            return accountDbo.Password.Equals(password);
        }

        public bool RegisterUser(AccountSavingDto accountData)
        {
            if (AccountWithUsernameExists(accountData.Username)) { return false; }
            if (AccountWithEmailExists(accountData.Email)) { return false; }
            _accountRepository.CreateAccount(new AccountDbo(accountData.Username, accountData.Email, accountData.Password, accountData.Description));
            return true;
        }

        public Task SendFriendRequest(string username, string request)
        {

            if (_accountRepository.GetAccountByUsername(username) == null)
            { return Task.CompletedTask; }

            if (_accountRepository.GetAccountByUsername(request) == null)
            { return Task.CompletedTask; }

            if (username.Equals(request)) { return Task.CompletedTask; }
        
            var requests = _friendRequestRepository.GetRequestsForUser(username);
            var friends = _friendRepository.GetFriendsForUser(username);
            if (requests.Contains(request)) { return Task.CompletedTask; }
            if (friends.Contains(request)) { return Task.CompletedTask; }

            this._friendRequestRepository.AddFriendRequest(username, request);
            return Task.CompletedTask;
        }

        public bool AccountExists(string username)
        {
            var account = _accountRepository.GetAccountByUsername(username);
            return account != null;
        }

        public bool UpdateEmail(string username, string email)
        {
            if (AccountWithEmailExists(email)) { return false; }
            _accountRepository.UpdateEmail(username, email);
            return true;
        }

        public bool UpdatePassword(string username, string password)
        {
            if (!AccountWithUsernameExists(username)) { return false;}

            var oldPassword = _accountRepository.GetAccountByUsername(username)!.Password;
            if(oldPassword!.Equals(password)) { return false; }
            
            _accountRepository.UpdatePassword(username, password);
            return true;
        }

        public bool UpdateUserDescription(string username, string description)
        {
            if (!AccountWithUsernameExists(username)) { return false; }

            _accountRepository.UpdateDescription(username, description);
            return true;
        }

        public bool UpdateUsername(string oldUsername, string newUsername)
        {
            if (!AccountWithUsernameExists(oldUsername)) { return false; }
            if (AccountWithUsernameExists(newUsername)) { return false; }
            _accountRepository.UpdateUsername(oldUsername, newUsername);
            return true;
        }

        public List<string> GetSentRequestsForUser(string username)
        {
            return _friendRequestRepository.GetSentRequestsForUser(username);
        }

        public Task RemoveFriend(string username, string friend)
        {
            _friendRepository.RemoveFriend(username, friend);
            _friendRepository.RemoveFriend(friend, username);
            return Task.CompletedTask;
        }

        #region privates
        private bool AccountWithUsernameExists(string username)
        {
            return _accountRepository.GetAccountByUsername(username) != null;
        }

        private bool AccountWithEmailExists(string email)
        {
            return _accountRepository.GetAccountByEmail(email) != null;
        }
        #endregion
    }
}
