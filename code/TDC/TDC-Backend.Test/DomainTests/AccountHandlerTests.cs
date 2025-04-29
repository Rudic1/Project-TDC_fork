using FluentAssertions;
using NSubstitute;
using TDC.Backend.DataRepository;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Test.DomainTests
{
    public class AccountHandlerTests
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
        public void GetAccountByUsername_CallsRepository_AndReturnsCorrectDto()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "test-email", "test-password", "test-description"));
            var expected = new AccountLoadingDto("test-user", "test-email", "test-description");

            var actual = _target.GetAccountByUsername("test-user");

            _target._accountRepository.Received().GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoginWithMail_CorrectMailAndPassword_CallsRepositoryAndReturnsTrue()
        {
            _target._accountRepository.GetAccountByEmail("test-email").Returns(new AccountDbo("test-user", "test-email", "test-password", "test-description"));

            var actual = _target.LoginWithMail("test-email", "test-password");

            _target._accountRepository.Received().GetAccountByEmail("test-email");
            actual.Should().BeTrue();
        }

        [Test]
        public void LoginWithMail_CorrectMailAndWrongPassword_CallsRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByEmail("test-email").Returns(new AccountDbo("test-user", "test-email", "test-password", "test-description"));

            var actual = _target.LoginWithMail("test-email", "other-password");

            _target._accountRepository.Received().GetAccountByEmail("test-email");
            actual.Should().BeFalse();
        }

        [Test]
        public void LoginWithMail_WrongMail_CallsRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByEmail("test-email").Returns((AccountDbo?)null);

            var actual = _target.LoginWithMail("test-email", "test-password");

            _target._accountRepository.Received().GetAccountByEmail("test-email");
            actual.Should().BeFalse();
        }

        [Test]
        public void LoginWithUsername_CorrectMailAndPassword_CallsRepositoryAndReturnsTrue()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "test-email", "test-password", "test-description"));

            var actual = _target.LoginWithUsername("test-user", "test-password");

            _target._accountRepository.Received().GetAccountByUsername("test-user");
            actual.Should().BeTrue();
        }

        [Test]
        public void LoginWithUsername_CorrectMailAndWrongPassword_CallsRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "test-email", "test-password", "test-description"));

            var actual = _target.LoginWithUsername("test-user", "other-password");

            _target._accountRepository.Received().GetAccountByUsername("test-user");
            actual.Should().BeFalse();
        }

        [Test]
        public void LoginWithUsername_WrongMail_CallsRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns((AccountDbo?) null);

            var actual = _target.LoginWithUsername("test-user", "test-password");

            _target._accountRepository.Received().GetAccountByUsername("test-user");
            actual.Should().BeFalse();
        }
    }
}
