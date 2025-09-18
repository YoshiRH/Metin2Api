using Metin2Api.Application.Dtos;
using Metin2Api.Application.Services;
using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;
using Moq;
using Xunit;

namespace Metin2Api.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _accountService = new AccountService(_accountRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAccountAsync_ShouldCreateAccount()
        {
            // Arrange
            var dto = new CreateAccountDto
            {
                Username = "testuser",
                Password = "password",
                Email = "test@mail.com"
            };

            // Act
            var result = await _accountService.AddAccountAsync(dto);

            _accountRepositoryMock.Verify(repo => repo.AddAccountAsync(It.IsAny<Account>()), Times.Once);
            _accountRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);

            // Assert
            Assert.Equal(dto.Username, result.Username);
            Assert.Equal(dto.Password, result.PasswordHash);
            Assert.Equal(dto.Email, result.Email);
            Assert.Empty(result.Characters);
        }

        [Fact]
        public async Task DeleteAccountAsync_ShouldReturnFalse_WhenAccountDoesNotExist()
        {
            // Arrange
            _accountRepositoryMock.Setup(repo => repo.GetAccountByIdAsync(1))
                .ReturnsAsync((Account?)null);

            // Act
            var result = await _accountService.DeleteAccountAsync(1);

            // Assert
            Assert.False(result);
            _accountRepositoryMock.Verify(repo => repo.DeleteAccountAsync(It.IsAny<Account>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAccountAsync_ShouldReturnTrue_WhenAccountExists()
        {
            // Arrange
            var account = new Account { Id = 1, Username = "testuser" };

            _accountRepositoryMock.Setup(repo => repo.GetAccountByIdAsync(1))
                .ReturnsAsync(account);

            // Act
            var result = await _accountService.DeleteAccountAsync(1);

            // Assert
            Assert.True(result);
            _accountRepositoryMock.Verify(repo => repo.DeleteAccountAsync(account), Times.Once);
        }

        [Fact]
        public async Task GetAccountByIdAsync_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            _accountRepositoryMock.Setup(repo => repo.GetAccountByIdAsync(1))
                .ReturnsAsync((Account?)null);

            // Act
            var result = await _accountService.GetAccountByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCharactersByAccountIdAsync_ShouldReturnEmpty_WhenAccountDoesNotExist()
        {
            // Arrange
            _accountRepositoryMock.Setup(repo => repo.GetAccountByIdAsync(1))
                .ReturnsAsync((Account?)null);

            // Act
            var result = await _accountService.GetCharactersByAccountIdAsync(1);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCharactersByAccountIdAsync_ShouldReturnCharacters_WhenAccountAndCharactersExists()
        {
            // Arrange
            var account = new Account
            {
                Id = 1,
                Username = "testuser"
            };

            var characters = new List<Character>
            {
                new Character { Id = 1, Nick = "Char1", Level = 10, Kingdom = Domain.Enums.Kingdom.Shinsoo, Class = Domain.Enums.CharacterClass.Warrior, AccountId = 1 },
                new Character { Id = 2, Nick = "Char2", Level = 20, Kingdom = Domain.Enums.Kingdom.Shinsoo, Class = Domain.Enums.CharacterClass.Ninja, AccountId = 1 }
            };

            _accountRepositoryMock.Setup(repo => repo.GetAccountByIdAsync(1))
                .ReturnsAsync(account);
            _accountRepositoryMock.Setup(repo => repo.GetCharactersByAccountIdAsync(1))
                .ReturnsAsync(characters);

            // Act
            var result = await _accountService.GetCharactersByAccountIdAsync(1);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Nick == "Char1");
        }

        [Fact]
        public async Task GetAllAccountsAsync_ShouldReturnEmpty_WhenNoneExist()
        {
            // Arrange
            _accountRepositoryMock.Setup(repo => repo.GettAllAccountsAsync())
                .ReturnsAsync(new List<Account>());

            // Act
            var result = await _accountService.GettAllAccountsAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAccountsAsync_ShouldReturnAccounts_WhenTheyExist()
        {
            // Arrange
            var accounts = new List<Account>
            {
                new Account { Id = 1, Username = "testuser1", PasswordHash = "123" , Email = "test1@mail.com", Characters = new List<Character>()},
                new Account { Id = 2, Username = "testuser2", PasswordHash = "321" , Email = "test2@mail.com", Characters = new List<Character>()}
            };

            _accountRepositoryMock.Setup(repo => repo.GettAllAccountsAsync())
                .ReturnsAsync(accounts);

            // Act
            var result = await _accountService.GettAllAccountsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, ac => ac.Username == "testuser1");
            Assert.Contains(result, ac => ac.Username == "testuser2");
        }
    }
}
