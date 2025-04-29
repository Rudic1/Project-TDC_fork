
using FluentAssertions;
using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class UpdateEmailTests
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
        public void UpdateEmail_EmailIsAvailable_CallsRepositoryAndReturnsTrue()
        {
            _target._accountRepository.GetAccountByEmail("new-email").Returns((AccountDbo?) null);

            var actual = _target.UpdateEmail("test-user", "new-email");

            _target._accountRepository.Received().UpdateEmail("test-user", "new-email");
            actual.Should().BeTrue();
        }

        [Test]
        public void UpdateEmail_EmailIsTaken_DoesNotCallRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByEmail("new-email").Returns(new AccountDbo("", "new-email", "", ""));

            var actual = _target.UpdateEmail("test-user", "new-email");

            _target._accountRepository.DidNotReceive().UpdateEmail(Arg.Any<string>(), Arg.Any<string>());
            actual.Should().BeFalse();
        }
    }
}
