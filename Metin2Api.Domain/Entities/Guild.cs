namespace Metin2Api.Domain.Entities
{
    public class Guild
    {
        // Guild properties
        public int Id { get; set; }
        public string GuildName { get; set; } = string.Empty;
        public int MasterId { get; set; }

        // Characters in guild
        public List<Character> Characters { get; set; } = new ();

    }
}
