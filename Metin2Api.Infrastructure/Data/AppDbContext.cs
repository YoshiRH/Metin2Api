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
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Guild> Guilds => Set<Guild>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // 1:N
            modelBuilder.Entity<Character>()
                .HasMany(i => i.Items)
                .WithOne(it => it.Character)
                .HasForeignKey(it => it.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1:N
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Characters)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1:1
            modelBuilder.Entity<Item>()
                .HasOne(c => c.Character)
                .WithMany(i => i.Items)
                .HasForeignKey(it => it.CharacterId);

            // 1:N
            modelBuilder.Entity<Guild>()
                .HasMany(c => c.Characters)
                .WithOne(g => g.Guild)
                .HasForeignKey(c => c.GuildId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
