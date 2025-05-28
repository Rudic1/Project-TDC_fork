using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class AcceptFriendRequestTests
    {
        private AccountHandler _target;
        private IAccountRepository _accountRepository;
        private IFriendRepository _friendRepository;
        private IFriendRequestRepository _friendRequestRepository;

        [SetUp]
        public void SetUp()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
            _friendRepository = Substitute.For<IFriendRepository>();
            _friendRequestRepository = Substitute.For<IFriendRequestRepository>();
            _target = new AccountHandler(_accountRepository, _friendRepository, _friendRequestRepository);

            _accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo());
            _accountRepository.GetAccountByUsername("test-request").Returns(new AccountDbo());
        }

        [Test]
        public void AcceptFriendRequest_UserIsNotFriendsWithRequestYet_CallsAddFriendForSenderAndReceiver() {
            _friendRepository.GetFriendsForUser("test-user").Returns([]);
            _friendRepository.GetFriendsForUser("test-request").Returns([]);

            _target.AcceptFriendRequest("test-user", "test-request");

            _target._friendRepository.Received().AddFriend("test-user", "test-request");
            _target._friendRepository.Received().AddFriend("test-request", "test-user");
        }

        [Test]
        public void AcceptFriendRequest_UserIsFriendsWithRequest_DoesNotCallRepository()
        {
            _friendRepository.GetFriendsForUser("test-user").Returns(["test-request"]);
            _friendRepository.GetFriendsForUser("test-request").Returns(["test-user"]);

            _target.AcceptFriendRequest("test-user", "test-request");

            _target._friendRepository.DidNotReceive().AddFriend(Arg.Any<string>(), Arg.Any<string>()); 
        }

        [Test]
        public void AcceptFriendRequest_UserIsNotFriendsWithRequestYet_CallsDeleteFriendRequestOnBoth()
        {
            _friendRepository.GetFriendsForUser("test-user").Returns([]);
            _friendRepository.GetFriendsForUser("test-request").Returns([]);

            _target.AcceptFriendRequest("test-user", "test-request");

            _target._friendRequestRepository.Received().DeleteFriendRequest("test-user", "test-request");
            _target._friendRequestRepository.Received().DeleteFriendRequest("test-request", "test-user");
        }

        [Test]
        public void AcceptFriendRequest_UserIsFriendsWithRequest_CallsRequestRepository()
        {
            _friendRepository.GetFriendsForUser("test-user").Returns(["test-request"]);
            _friendRepository.GetFriendsForUser("test-request").Returns([]);

            _target.AcceptFriendRequest("test-user", "test-request");

            _target._friendRequestRepository.Received().DeleteFriendRequest("test-user", "test-request");
            _target._friendRequestRepository.Received().DeleteFriendRequest("test-request", "test-user");
        }
    }
}
