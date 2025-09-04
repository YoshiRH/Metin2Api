using Microsoft.EntityFrameworkCore;
using Metin2Api.Domain.Entities;


namespace Metin2Api.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<IItem> Items => Set<IItem>();
        public DbSet<Inventory> Inventories => Set<Inventory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1:1
            modelBuilder.Entity<Character>()
                .HasOne(c => c.Inventory)
                .WithOne(i => i.Character)
                .HasForeignKey<Inventory>(i => i.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1:N
            modelBuilder.Entity<Inventory>()
                .HasMany(i => i.Items)
                .WithOne(it => it.Inventory)
                .HasForeignKey(it => it.InventoryId);

            // 1:N
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Characters)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
