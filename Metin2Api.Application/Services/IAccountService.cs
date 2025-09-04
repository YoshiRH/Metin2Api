using Metin2Api.Application.Dtos;
using Metin2Api.Domain.Entities;

namespace Metin2Api.Application.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GettAllAccountsAsync();
        Task<IEnumerable<CharacterDto>> GetCharactersByAccountIdAsync(int accountId);
        Task<AccountDto?> GetAccountByIdAsync(int accountId);
        Task<Account> AddAccountAsync(CreateAccountDto account);
        Task<bool> DeleteAccountAsync(AccountDto account);
    }
}
