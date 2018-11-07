using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vue24Hour.Models;
using Vue24Hour.Services;

namespace Vue24Hour.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IAccountService _accountService;
        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // POST /api/login
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginModel newAccount)
        {
            if (string.IsNullOrEmpty(newAccount?.Phone))
            {
                return BadRequest("Telefoonnummer mag niet leeg zijn.");
            }

            if (string.IsNullOrEmpty(newAccount.Password))
            {
                return BadRequest("Wachtwoord mag niet leeg zijn.");
            }

            var result = await _accountService.GetAccount(newAccount.Phone, newAccount.Password);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Kan niet inloggen");
            }
        }
    }
}
