using ClientSVH.Application.Services;
using ClientSVH.Contracts;
using ClientSVH.Core.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;


namespace ClientSVH.Controllers
{
    [ApiController]
    [Route("svh/packages")]
    public class PackagesController(IPackagesServices pkgService, IWebHostEnvironment appEnvironment,
                              IUsersService userService) : ControllerBase
    {
        private readonly IPackagesServices _pkgService = pkgService;
        private readonly IWebHostEnvironment _appEnvironment = appEnvironment;
        private readonly IUsersService _userService = userService;

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
        [HttpPost("send")]
        public async Task<IActionResult> SendToServer(PkgSendResponse pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            await _pkgService.SendToServer(pkgSend.Pid);

            return Ok();
        }
    }
}
