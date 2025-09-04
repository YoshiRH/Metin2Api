namespace Metin2Api.Application.Dtos
{
    public class CreateAccountDto
    {
        // Account properties
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
