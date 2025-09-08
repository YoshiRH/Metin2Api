using Metin2Api.Domain.Entities;

namespace Metin2Api.Application.Dtos
{
    public class CreateGuildDto
    {
        public string Name { get; set; } = string.Empty;
        public int MasterId { get; set; }
    }
}
