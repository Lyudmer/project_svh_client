using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ClientSVH.Core.Abstaction.Services;
using ClientSVH.Contracts;
using ClientSVH.Application.Services;

namespace ClientSVH.Controllers
{
    [ApiController]
    [Route("svh/account")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IWebHostEnvironment _appEnvironment;
        public UsersController(IUsersService usersService, IWebHostEnvironment appEnvironment)
        {
            _usersService = usersService;
            _appEnvironment = appEnvironment;

        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserRequest userLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _usersService.Login(userLogin.passwordHash, userLogin.email);

            return Ok(result);
          
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest userRegistr)
        {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _usersService.Register(userRegistr.username, userRegistr.passwordHash, userRegistr.email);
            return Ok();
        }
      
       

    }
}
