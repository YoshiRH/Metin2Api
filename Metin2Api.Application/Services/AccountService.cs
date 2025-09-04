using Metin2Api.Application.Dtos;
using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Metin2Api.Application.Services
{
    public class AccountService(IAccountRepository accountRepository) : IAccountService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        public async Task<Account> AddAccountAsync(CreateAccountDto account)
        {
            if(account == null)
                throw new ArgumentNullException(nameof(account));

            var newAccount = new Account
            {
                Username = account.Username,
                PasswordHash = account.Password, // Well there should be some hashing method, maybe later
                Email = account.Email,
                Characters = new List<Character>()
            };

            await _accountRepository.AddAccountAsync(newAccount);
            await _accountRepository.SaveChangesAsync();

            return newAccount;
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var existingAccount = await _accountRepository.GetAccountByIdAsync(id);

            if (existingAccount == null)
                return false;

            await _accountRepository.DeleteAccountAsync(existingAccount);
            await _accountRepository.SaveChangesAsync();

            return true;
        }

        public async Task<AccountDto?> GetAccountByIdAsync(int accountId)
        {
            var existingAccount = await _accountRepository.GetAccountByIdAsync(accountId);

            if (existingAccount == null) return null;

            var returnedAccount = new AccountDto
            {
                Id = accountId,
                Username = existingAccount.Username,
                Email = existingAccount.Email,
            };

            return returnedAccount; 
        }

        public async Task<IEnumerable<CharacterDto>> GetCharactersByAccountIdAsync(int accountId)
        {
            var existingAccount = await _accountRepository.GetAccountByIdAsync(accountId);
            
            if(existingAccount == null)
                return Enumerable.Empty<CharacterDto>();

            var returnedCharacters = await _accountRepository.GetCharactersByAccountIdAsync(accountId);

            if(returnedCharacters == null)
                return Enumerable.Empty<CharacterDto>();

            var formatedCharacters = returnedCharacters
                .Select(c => new CharacterDto
                {
                    Id = c.Id,
                    Nick = c.Nick,
                    Level = c.Level,
                    Kingdom = c.Kingdom,
                    Class = c.Class,
                    AccountId = c.AccountId
                }).ToList();

            return formatedCharacters;
        }

        public async Task<IEnumerable<AccountDto>> GettAllAccountsAsync()
        {
            var allAccounts = await _accountRepository.GettAllAccountsAsync();

            if(allAccounts == null)
                return Enumerable.Empty<AccountDto>();

            var formatedAccounts = allAccounts
                .Select(a => new AccountDto
                {
                    Id = a.Id,
                    Username = a.Username,
                    Email = a.Email
                });
            
            return formatedAccounts;
        }
    }
}
