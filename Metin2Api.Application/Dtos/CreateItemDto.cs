namespace Metin2Api.Application.Dtos
{
    public class CreateItemDto
    {
        // Common Item Properties
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Value { get; set; };
    }
}
