using Metin2Api.Application.Dtos;
using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;

namespace Metin2Api.Application.Services
{
    public class CharacterService(ICharacterRepository characterRepository, IAccountRepository accountRepository) : ICharacterService
    {

        private readonly ICharacterRepository _characterRepository = characterRepository;
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task<string> AddCharacterToAccountAsync(CreateCharacterDto character, int accountId)
        {
            if(character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            var account = await _accountRepository.GetAccountByIdAsync(accountId);

            if (account == null)
                return "Account not found";

            if (account.Characters.Count >= 4)
                return "Account already has 4 characters";

            var newCharacter = new Character
            {
                Nick = character.Nick,
                Level = 1,
                Kingdom = character.Kingdom,
                Class = character.Class,
                AccountId = accountId,
                Inventory = null
            };

            await _characterRepository.AddCharacterAsync(newCharacter);
            await _characterRepository.SaveChangesAsync();

            return "Character created";
        }

        public async Task<bool> DeleteCharacterAsync(int charactedId)
        {
            var character = await _characterRepository.GetCharacterByIdAsync(charactedId);

            if (character == null)
                return false;

            await _characterRepository.DeleteCharacterAsync(character.Id);
            await _characterRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CharacterDto>> GetAllCharactersAsync()
        {
            var characters = await _characterRepository.GetAllCharactersAsync();

            if( characters == null )
                return Enumerable.Empty<CharacterDto>();

            var formatedCharacters = characters
                .Select(c => new CharacterDto
                {
                    Id = c.Id,
                    Nick = c.Nick,
                    Level = c.Level,
                    Kingdom = c.Kingdom,
                    Class = c.Class,
                    AccountId = c.AccountId,
                });

            return formatedCharacters;
        }

        public async Task<CharacterDto?> GetCharacterByIdAsync(int id)
        {
            var character = await _characterRepository.GetCharacterByIdAsync(id);

            if(character == null)
                throw new ArgumentNullException();

            var formatedCharacter = new CharacterDto
            {
                Id=character.Id,
                Nick = character.Nick,
                Level = character.Level,
                Kingdom = character.Kingdom,
                Class = character.Class,
                AccountId = character.AccountId,
            };
            
            return formatedCharacter;
        }

        public async Task<CharacterDto?> GetCharacterWithBiggestLvl()
        {
            var character = await _characterRepository.GetCharacterWithBiggestLvl();

            if (character == null)
                throw new ArgumentNullException();

            var formatedCharacter = new CharacterDto
            {
                Id = character.Id,
                Nick = character.Nick,
                Level = character.Level,
                Kingdom = character.Kingdom,
                Class = character.Class,
                AccountId = character.AccountId,
            };

            return formatedCharacter;
        }


        public async Task<IEnumerable<CharacterDto>> GetTop10CharactersByLevelAsync()
        {
            var characters = await _characterRepository.GetTop10CharactersByLevelAsync();

            if (characters == null)
                throw new ArgumentNullException();

            var formatedCharacters = characters
                .Select(c => new CharacterDto
            {
                Id = c.Id,
                Nick = c.Nick,
                Level = c.Level,
                Kingdom = c.Kingdom,
                Class = c.Class,
                AccountId = c.AccountId
            });

            return formatedCharacters;
        }
    }
}
