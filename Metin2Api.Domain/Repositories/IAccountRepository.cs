using Metin2Api.Domain.Entities;

namespace Metin2Api.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GettAllAccountsAsync();
        Task<IEnumerable<Character>> GetCharactersByAccountIdAsync(int accountId);
        Task<Account?> GetAccountByIdAsync(int accountId);
        Task AddAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);
        Task SaveChangesAsync();
    }
}
