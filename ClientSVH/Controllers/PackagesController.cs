
using ClientSVH.Application.Services;
using ClientSVH.Contracts;
using ClientSVH.Core.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;


namespace ClientSVH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var UserId =_userService.GetUserId().ToString();
            if (Guid.TryParse(UserId, out var userId))
            {
                await _pkgService.LoadFile(userId, filePath);
            }
            return Ok();
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendToServer(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            await _pkgService.SendToServer(pkgSend.Pid);

            return Ok();
        }
        [HttpPost("GetHistory")]
        public async Task<IActionResult> GetHistoryPkg(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           var result= await _pkgService.HistoriPkgByPid(pkgSend.Pid);

            return Ok(result);
        }
        [HttpPost("GetPackage")]
        public async Task<IActionResult> GetPkgId(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _pkgService.GetPkgId(pkgSend.Pid);

            return Ok(result);
        } 
        [HttpPost("GetDocsPackage")]
        public async Task<IActionResult> GetDocsPkg(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _pkgService.GetDocsPkg(pkgSend.Pid);

            return Ok(result);
        }
        [HttpPost("DelPackage")]
        public async Task<IActionResult> DeletePkg(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _pkgService.DeletePkg(pkgSend.Pid);

            return Ok();
        }
        [HttpPost("sendDelPkg")]
        public async Task<IActionResult> SendDelPkgToServer(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _pkgService.SendDelPkgToServer(pkgSend.Pid);

            return Ok();
        }
    }
}
