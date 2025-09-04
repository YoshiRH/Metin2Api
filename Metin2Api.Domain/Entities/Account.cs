namespace Metin2Api.Domain.Entities
{
    public class Account
    {
        // Account properties
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Characters in account
        public List<Character> Characters { get; set; } = new();

    }
}
