using Metin2Api.Domain.Entities;

namespace Metin2Api.Application.Dtos
{
    public class AccountDto
    {
        // Account properties
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
