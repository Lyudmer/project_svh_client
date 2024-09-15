using ClientSVH.Application.Services;
using ClientSVH.Core.Abstaction.Services;
using Microsoft.AspNetCore.Mvc;


namespace ClientSVH.Controllers
{
    [ApiController]
    [Route("api/[controller]/[user]")]
    public class PackagesController : ControllerBase
    {
        private readonly IPackagesServices _pkgService;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IUsersService _userService;
        public PackagesController(IPackagesServices pkgService, IWebHostEnvironment appEnvironment, IUsersService userService)
        {
            _pkgService = pkgService;
            _appEnvironment = appEnvironment;
            _userService = userService;            

        }
        [HttpPost("loadfile")]
        public async Task<IActionResult> LoadFile(IFormFile InName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var uploads = _appEnvironment.WebRootPath;
            var filePath = Path.Combine(uploads, InName.FileName).ToString();
            await _pkgService.LoadFile( _userService.GetUserIdFromLogin(), filePath);

            return Ok();
        }
    }
}
