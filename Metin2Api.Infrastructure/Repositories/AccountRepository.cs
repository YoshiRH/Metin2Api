using Microsoft.EntityFrameworkCore;
using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;
using Metin2Api.Infrastructure.Data;

namespace Metin2Api.Infrastructure.Repositories
{
    public class AccountRepository(AppDbContext context) : IAccountRepository
    {
        private readonly AppDbContext _context = context;
        public async Task AddAccountAsync(Account account)
        {
            if(account == null)
                throw new ArgumentNullException(nameof(account));

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAccountAsync(Account account)
        {
            if(account == null)
                throw new ArgumentNullException(nameof(account));

            _context.Accounts.Remove(account);
            return _context.SaveChangesAsync();
        }

        public async Task<Account?> GetAccountByIdAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if(account == null)
                return null;

            return account;
        }

        public async Task<IEnumerable<Character>> GetCharactersByAccountIdAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if (account == null)
                return new List<Character>();

            var characters = await _context.Characters.Where(c => c.AccountId == accountId).ToListAsync();

            if(characters == null || characters.Count == 0)
                return new List<Character>();

            return characters;
        }

        public async Task<IEnumerable<Account>> GettAllAccountsAsync()
        {
            var accounts = await _context.Accounts.ToListAsync();

            if(accounts == null || accounts.Count == 0)
                return new List<Account>();

            return accounts;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
