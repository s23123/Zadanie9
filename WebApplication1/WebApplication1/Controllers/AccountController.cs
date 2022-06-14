using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication1.Models.DTO;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountsDbService _accountsDbService;

        public AccountController(IAccountsDbService accountsDbService)
        {
            _accountsDbService = accountsDbService;
        }


        [HttpPost("updateToken")]
        public async Task<IActionResult> UpdateToken(SomeSortOfToken someSortOfToken)
        {
            try
            {
                var res = await _accountsDbService.RefreshToken(someSortOfToken);
                return Ok(res);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddUser(SomeSortOfUser someSortOfUser)
        {
            
                
            try
            {
                await _accountsDbService.Register(someSortOfUser);
                return Ok("Pomyslnie dodano");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
                
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(SomeSortOfUser someSortOfUser)
        {
            try
            {
                var result = await _accountsDbService.Login(someSortOfUser);
                return Ok(result);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
