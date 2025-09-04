using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Metin2Api.Application.Services;
using Metin2Api.Application.Dtos;


namespace Metin2Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;

        [HttpPost("CreateAccount")]
        public async Task<ActionResult> CreateAccount(CreateAccountDto accountDto)
        {
            var account = await _accountService.AddAccountAsync(accountDto);

            return Ok(account.Id);
        }

        [HttpDelete("Accounts/{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            var result = await _accountService.DeleteAccountAsync(id);

            return result ? Ok(result) : NotFound();
        }

        [HttpGet("Accounts")]
        public async Task<ActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GettAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("Accounts/{id}")]
        public async Task<ActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            return Ok(account);
        }

        [HttpGet("Accounts/{id}/Characters")]
        public async Task<ActionResult> GetAccountCharacters(int id)
        {
            var characters = await _accountService.GetCharactersByAccountIdAsync(id);
            return Ok(characters);
        }
    }
}
