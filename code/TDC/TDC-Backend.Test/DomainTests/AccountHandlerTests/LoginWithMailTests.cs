using FluentAssertions;
using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class LoginWithMailTests
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
    }
}
