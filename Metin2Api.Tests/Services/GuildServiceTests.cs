using Metin2Api.Application.Dtos;
using Metin2Api.Application.Services;
using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;
using Moq;

namespace Metin2Api.Tests.Services
{
    public class GuildServiceTests
    {
        private readonly Mock<IGuildRepository> _guildRepositoryMock;
        private readonly Mock<ICharacterRepository> _characterRepositoryMock;
        private readonly GuildService _guildService;

        public GuildServiceTests()
        {
            _guildRepositoryMock = new Mock<IGuildRepository>();
            _characterRepositoryMock = new Mock<ICharacterRepository>();
            _guildService = new GuildService(_guildRepositoryMock.Object, _characterRepositoryMock.Object);
        }

        [Fact]
        public async Task AddCharacterToGuildAsync_ShouldReturnError_WhenGuildNotFound()
        {
            // Arrange
            _guildRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Guild?)null);

            // Act
            var result = await _guildService.AddCharacterToGuildAsync(1, 1);

            // Assert
            Assert.Equal("Guild not found.", result);
        }

        [Fact]
        public async Task AddCharacterToGuildAsync_ShouldReturnError_WhenCharacterNotFound()
        {
            // Arrange
            _guildRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new Guild());
            _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(1)).ReturnsAsync((Character?)null);

            // Act
            var result = await _guildService.AddCharacterToGuildAsync(1, 1);

            // Assert
            Assert.Equal("Character not found.", result);
        }

        [Fact]
        public async Task AddCharacterToGuildAsync_ShouldReturnError_WhenCharacterAlreadyInGuild()
        {
            // Arrange
            var character = new Character { Id = 1, GuildId = 2 };
            _guildRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new Guild());
            _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(1)).ReturnsAsync(character);

            // Act
            var result = await _guildService.AddCharacterToGuildAsync(1, 1);

            // Assert
            Assert.Equal("Character is already in a guild.", result);
        }

        [Fact]
        public async Task AddCharacterToGuildAsync_ShouldAddCharacterToGuild_WhenValidInput()
        {
            // Arrange
            var character = new Character { Id = 1, GuildId = null };
            var guild = new Guild { Id = 1 };

            _guildRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(guild);
            _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(1)).ReturnsAsync(character);

            // Act
            var result = await _guildService.AddCharacterToGuildAsync(1, 1);

            // Assert
            Assert.Equal("Character added to guild successfully.", result);

            _guildRepositoryMock.Verify(repo => repo.AddCharacterToGuildAsync(1, character), Times.Once);
            _guildRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            _characterRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);

            Assert.Equal(1, character.GuildId);
        }

        [Fact]
        public async Task CreateGuildAsync_ShouldReturnError_WhenNameIsEmpty()
        {
            // Act
            var result = await _guildService.CreateGuildAsync(new CreateGuildDto { Name = "   ", MasterId = 1 });

            // Assert
            Assert.Equal("Guild name cannot be empty.", result);
        }

        [Fact]
        public async Task CreateGuildAsync_ShouldReturnError_WhenNameAlreadyExists()
        {
            // Arrange
            _guildRepositoryMock.Setup(repo => repo.GetByNameAsync("ExistingGuild")).ReturnsAsync(new Guild());

            var newGuild = new CreateGuildDto { Name = "ExistingGuild", MasterId = 1 };

            // Act
            var result = await _guildService.CreateGuildAsync(newGuild);

            // Assert
            Assert.Equal("Guild name already exists.", result);
        }

        [Fact]
        public async Task CreateGuildAsync_ShouldReturnError_WhenMasterNotFound()
        {
            // Arrange
            _guildRepositoryMock.Setup(repo => repo.GetByNameAsync("NewGuild")).ReturnsAsync((Guild?)null);
            _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(1)).ReturnsAsync((Character?)null);

            var newGuild = new CreateGuildDto { Name = "NewGuild", MasterId = 1 };

            // Act
            var result = await _guildService.CreateGuildAsync(newGuild);

            // Assert
            Assert.Equal("Master character not found.", result);
        }
    }
}
