namespace Metin2Api.Domain.Entities
{
    public class WeaponItem : IItem
    {
        // Item Properties
        public int Id { get; set ; }
        public string Name { get ; set; } = string.Empty;
        public int AttackPower { get; set; }
        public int requiredLevel { get; set; }
    }
}
