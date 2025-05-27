using Microsoft.AspNetCore.Mvc;
using TDC.Backend.Helpers;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountHandler accountHandler) : ControllerBase
    {
        internal readonly IAccountHandler _accountHandler = accountHandler;

        [HttpPut("registerUser")]
        public bool RegisterUser([FromBody] AccountSavingDto accountData)
        {
            return _accountHandler.RegisterUser(accountData);
        }

        [HttpPost("updateUsername/{username}/{newUsername}")]
        public bool UpdateUsername([FromRoute] string username, [FromRoute] string newUsername)
        {
            return _accountHandler.UpdateUsername(username, newUsername);
        }

        [HttpPost("updateUserDescription/{username}")]
        public bool UpdateUserDescription([FromRoute] string username, [FromBody] DescriptionHelper description)
        {
            return _accountHandler.UpdateUserDescription(username, description.Description);
        }

        [HttpPost("updateEmail/{username}")]
        public bool UpdateEmail([FromRoute] string username, [FromBody] MailHelper email)
        {
            return _accountHandler.UpdateEmail(username, email.Email);
        }

        [HttpPost("updatePassword/{username}")]
        public bool UpdatePassword([FromRoute] string username, [FromBody] PasswordHelper password)
        {
            return _accountHandler.UpdatePassword(username, password.Password);
        }

        [HttpPost("logInWithMail")]
        public bool LoginWithMail([FromBody] MailLoginHelper loginData)
        {
            return _accountHandler.LoginWithMail(loginData.Email, loginData.Password);
        }

        [HttpPost("logInWithUsername")]
        public bool LoginWithUsername([FromBody] UsernameLoginHelper loginData)
        {
            return _accountHandler.LoginWithUsername(loginData.Username, loginData.Password);
        }

        [HttpGet("getAccountData/{username}")]
        public AccountLoadingDto GetAccountByUsername([FromRoute] string username)
        {
            return _accountHandler.GetAccountByUsername(username);
        }

        [HttpGet("accountExists/{username}")]
        public bool AccountExists([FromRoute] string username)
        {
            return _accountHandler.AccountExists(username);
        }

        #region Friend Management

        [HttpGet("getFriendsForUser/{username}")]
        public List<string> GetFriendsForUser([FromRoute] string username)
        {
            return this._accountHandler.GetFriendsForUser(username);
        }

        [HttpGet("getFriendRequestsForUser/{username}")]
        public List<string> GetFriendRequestsForUser([FromRoute] string username)
        {
            return this._accountHandler.GetRequestsForUser(username);
        }

        [HttpGet("getSentFriendRequestsForUser/{username}")]
        public List<string> GetSentFriendRequestsForUser([FromRoute] string username)
        {
            return this._accountHandler.GetSentRequestsForUser(username);
        }

        [HttpPost("acceptFriendRequest/{username}/{request}")]
        public async Task AcceptFriendRequest([FromRoute] string username, [FromRoute] string request)
        {
            await this._accountHandler.AcceptFriendRequest(username, request);
        }

        [HttpPost("denyFriendRequest/{username}/{request}")]
        public async Task DenyFriendRequest([FromRoute] string username, [FromRoute] string request)
        {
            await this._accountHandler.DenyFriendRequest(username, request);
        }

        [HttpPut("sendFriendRequest/{username}/{request}")]
        public async Task SendFriendRequest([FromRoute] string username, [FromRoute] string request)
        {
            await this._accountHandler.SendFriendRequest(username, request);
        }

        [HttpPut("cancelFriendRequest/{username}/{request}")]
        public async Task CancelFriendRequest([FromRoute] string username, [FromRoute] string request)
        {
            await this._accountHandler.CancelFriendRequest(username, request);
        }

        [HttpPost("removeFriend/{username}/{friend}")]
        public async Task RemoveFriend([FromRoute] string username, [FromRoute] string friend)
        {
            await this._accountHandler.RemoveFriend(username, friend);
        }
        #endregion
    }
}
