using Metin2Api.Application.Dtos;
using Metin2Api.Domain.Repositories;
using Metin2Api.Domain.Entities;
using Metin2Api.Application.Services;
using Moq;
using Xunit;

namespace Metin2Api.Tests.Services
{
    public class ItemServiceTests
    {
        private readonly Mock<IItemRepository> _itemRepositoryMock;
        private readonly Mock<ICharacterRepository> _characterRepositoryMock;
        private readonly ItemService _itemService;

        public ItemServiceTests()
        {
            _itemRepositoryMock = new Mock<IItemRepository>();
            _characterRepositoryMock = new Mock<ICharacterRepository>();
            _itemService = new ItemService(_itemRepositoryMock.Object, _characterRepositoryMock.Object);
        }

        [Fact]
        public async Task AddItemToCharacterAsync_ShouldReturnError_WhenCharacterDoesNotExist()
        {
            // Arrange
            _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(1))
                .ReturnsAsync((Character?)null);

            var newItem = new CreateItemDto { Name = "ExampleItem", Value = 10 };

            // Act
            var result = await _itemService.AddItemToCharacterAsync(newItem, 1);

            // Assert
            Assert.Equal("Character doesn't exist", result);
        }

        [Fact]
        public async Task AddItemToCharacterAsync_ShouldReturnError_WhenItemIsNull()
        {
            // Arrange
            _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(1))
                .ReturnsAsync(new Character { Id = 1, Nick = "Hero", Kingdom = Domain.Enums.Kingdom.Shinsoo, Class = Domain.Enums.CharacterClass.Warrior });

            // Act
            var result = await _itemService.AddItemToCharacterAsync(null!, 1);

            // Assert
            Assert.Equal("Item missing", result);
        }

        [Fact]
        public async Task AddItemToCharacterAsync_ShouldReturnSuccess_WhenItemIsAdded()
        {
            // Arrange
            var character = new Character { Id = 1, Nick = "Hero", Kingdom = Domain.Enums.Kingdom.Shinsoo, Class = Domain.Enums.CharacterClass.Warrior };

            _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(1))
                .ReturnsAsync(character);

            var newItem = new CreateItemDto { Name = "ExampleItem", Value = 10 };

            _itemRepositoryMock.Setup(repo => repo.AddItemToCharacterAsync(1, It.IsAny<Item>()))
                .ReturnsAsync(new Item { Id = 1, Name = "ExampleItem", Value = 10, CharacterId = 1 });

            // Act
            var result = await _itemService.AddItemToCharacterAsync(newItem, 1);

            // Assert
            Assert.Equal("Item added", result);
        }

        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnEmptyList_WhenNoItemsExist()
        {
            // Arrange
            _itemRepositoryMock.Setup(repo => repo.GetAllItemsAsync())
                .ReturnsAsync((IEnumerable<Item>?)null);

            // Act
            var result = await _itemService.GetAllItemsAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnItems_WhenItemsExist()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item { Id = 1, Name = "Item1", Value = 10, CharacterId = 1 },
                new Item { Id = 2, Name = "Item2", Value = 20, CharacterId = 1 }
            };

            _itemRepositoryMock.Setup(repo => repo.GetAllItemsAsync())
                .ReturnsAsync(items);

            // Act
            var result = await _itemService.GetAllItemsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, i => i.Name == "Item1");
            Assert.Contains(result, i => i.Name == "Item2");
        }
    }
}
