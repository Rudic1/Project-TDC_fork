using FluentAssertions;
using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class UpdateUserDescriptionTests
    {
        private AccountHandler _target;
        private IAccountRepository _accountRepository;

        [SetUp]
        public void SetUp()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
            _target = new AccountHandler(_accountRepository);
        }

        [Test]
        public void UpdateUserDescription_UserExists_CallsRepositoryAndReturnsTrue()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "", "", ""));

            var actual = _target.UpdateUserDescription("test-user", "new-description");

            _target._accountRepository.Received().UpdateDescription("test-user", "new-description");
            actual.Should().BeTrue();
        }

        [Test]
        public void UpdateUserDescription_UserDoesNotExist_DoesNotCallRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns((AccountDbo?) null);

            var actual = _target.UpdateUserDescription("test-user", "new-description");

            _target._accountRepository.DidNotReceive().UpdateDescription(Arg.Any<string>(), Arg.Any<string>());
            actual.Should().BeFalse();
        }
    }
}
