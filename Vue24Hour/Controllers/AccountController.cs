using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vue24Hour.Models;
using Vue24Hour.Services;

namespace Vue24Hour.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Handles GET /api/account
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetItems();
            return Ok(accounts);
        }

        // POST /api/account
        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] AccountModel newAccount)
        {
            if (string.IsNullOrEmpty(newAccount?.Phone))
            {
                return BadRequest("Telefoonnummer mag niet leeg zijn.");
            }

            if (string.IsNullOrEmpty(newAccount.Password))
            {
                return BadRequest("Wachtwoord mag niet leeg zijn.");
            }

            if (string.IsNullOrEmpty(newAccount.Password2))
            {
                return BadRequest("Wachtwoord mag niet leeg zijn.");
            }

            if (string.IsNullOrEmpty(newAccount.Name))
            {
                return BadRequest("Naam mag niet leeg zijn.");
            }

            if (newAccount.Password != newAccount.Password2)
            {
                return BadRequest("Wachtwoorden zijn niet gelijk.");
            }

            await _accountService.AddAccount(newAccount.Phone, newAccount.Password2, newAccount.Name);

            return Ok();
        }
    }
}
