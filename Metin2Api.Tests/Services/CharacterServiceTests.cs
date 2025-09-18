using Metin2Api.Application.Dtos;
using Metin2Api.Application.Services;
using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;
using Moq;
using Xunit;

namespace Metin2Api.Tests.Services
{
    public class CharacterServiceTests
    {
        private readonly Mock<ICharacterRepository> _characterRepositoryMock;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly CharacterService _characterService;

        public CharacterServiceTests()
        {
            _characterRepositoryMock = new Mock<ICharacterRepository>();
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _characterService = new CharacterService(_characterRepositoryMock.Object, _accountRepositoryMock.Object);
        }

        [Fact]
        public async Task AddCharacterToAccountAsync_ShouldCreateCharacter_WhenAccountExistsAndHasLessThan4Characters()
        {
            // Arrange
            var dto = new CreateCharacterDto
            {
                Nick = "Hero",
                Kingdom = Domain.Enums.Kingdom.Shinsoo,
                Class = Domain.Enums.CharacterClass.Warrior
            };

            var account = new Account
            {
                Id = 1,
                Username = "testuser",
                Characters = new List<Character>()
            };

            _accountRepositoryMock.Setup(repo => repo.GetAccountByIdAsync(1))
                .ReturnsAsync(account);

            // Act
            var result = await _characterService.AddCharacterToAccountAsync(dto, 1);

            // Assert
            Assert.Equal("Character created", result);
            _characterRepositoryMock.Verify(repo => repo.AddCharacterAsync(It.IsAny<Character>()), Times.Once);
            _characterRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddCharacterToAccountAsync_ShouldReturnAccountNotFound_WhenAccountDoesNotExist()
        {
            // Arrange
            var dto = new CreateCharacterDto
            {
                Nick = "Hero",
                Kingdom = Domain.Enums.Kingdom.Shinsoo,
                Class = Domain.Enums.CharacterClass.Warrior
            };

            _accountRepositoryMock.Setup(repo => repo.GetAccountByIdAsync(1))
                .ReturnsAsync((Account?)null);

            // Act
            var result = await _characterService.AddCharacterToAccountAsync(dto, 1);

            // Assert
            Assert.Equal("Account not found", result);
            _characterRepositoryMock.Verify(repo => repo.AddCharacterAsync(It.IsAny<Character>()), Times.Never);
        }

        [Fact]
        public async Task AddCharacterToAccountAsync_ShouldReturnAccountAlreadyHas4Characters_WhenAccountHas4Characters()
        {
            // Arrange
            var dto = new CreateCharacterDto
            {
                Nick = "Hero",
                Kingdom = Domain.Enums.Kingdom.Shinsoo,
                Class = Domain.Enums.CharacterClass.Warrior
            };

            var account = new Account
            {
                Id = 1,
                Username = "testuser",
                Characters = new List<Character>
                {
                    new Character(),
                    new Character(),
                    new Character(),
                    new Character()
                }
            };

            _accountRepositoryMock.Setup(repo => repo.GetAccountByIdAsync(1))
                .ReturnsAsync(account);

            // Act
            var result = await _characterService.AddCharacterToAccountAsync(dto, 1);

            // Assert
            Assert.Equal("Account already has 4 characters", result);
            _characterRepositoryMock.Verify(repo => repo.AddCharacterAsync(It.IsAny<Character>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCharacterAsync_ShouldReturnFalse_WhenCharacterDoesNotExist()
        {
            // Arrange
            _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(1))
                .ReturnsAsync((Character?)null);

            // Act
            var result = await _characterService.DeleteCharacterAsync(1);

            // Assert
            Assert.False(result);
            _characterRepositoryMock.Verify(repo => repo.DeleteCharacterAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCharacterAsync_ShouldReturnTrue_WhenCharacterExists()
        {
            // Arrange
            var character = new Character { Id = 1, Nick = "Hero", Kingdom = Domain.Enums.Kingdom.Shinsoo, Class = Domain.Enums.CharacterClass.Warrior };
            _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(1))
                .ReturnsAsync(character);

            // Act
            var result = await _characterService.DeleteCharacterAsync(1);

            // Assert
            Assert.True(result);
            _characterRepositoryMock.Verify(repo => repo.DeleteCharacterAsync(character.Id), Times.Once);
            _characterRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllCharactersAsync_ShouldReturnAllCharacters()
        {
            // Arrange
            var characters = new List<Character>
            {
                new Character { Id = 1, Nick = "Hero1", Kingdom = Domain.Enums.Kingdom.Shinsoo, Class = Domain.Enums.CharacterClass.Warrior },
                new Character { Id = 2, Nick = "Hero2", Kingdom = Domain.Enums.Kingdom.Shinsoo, Class = Domain.Enums.CharacterClass.Ninja }
            };

            _characterRepositoryMock.Setup(repo => repo.GetAllCharactersAsync())
                .ReturnsAsync(characters);

            // Act
            var result = await _characterService.GetAllCharactersAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Nick == "Hero1");
            Assert.Contains(result, c => c.Nick == "Hero2");
        }
    }
}
