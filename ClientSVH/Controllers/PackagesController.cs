using Microsoft.AspNetCore.Hosting;
using ClientSVH.Contracts;
using ClientSVH.Core.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Xml.Linq;
using SharpCompress.Compressors.Xz;
using System.Text;
using Microsoft.AspNetCore.Http;
using ClientSVH.Infrastructure;
using ClientSVH.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace ClientSVH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackagesController(IPackagesServices pkgService,
                                    IWebHostEnvironment webHostEnvironment): ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly IPackagesServices _pkgService = pkgService;
       

        [HttpPost("LoadFile")]
        public async Task<IActionResult> LoadFile(IFormFile InName,string UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + InName.FileName, FileMode.Create))
            {
                await InName.CopyToAsync(fileStream);
                fileStream.Position = 0;
                using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    var resFile = reader.ReadToEnd();
                    reader.Close();

                    if (resFile.Length>0 && Guid.TryParse(UserId, out var userId))
                    {
                        await _pkgService.LoadFile(userId, resFile);
                    }
                }
            }
            return Ok();
        }
        [HttpPost("SendToServer")]
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
        [HttpPost("SendToServerDelPkg")]
        public async Task<IActionResult> SendDelPkgToServer(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _pkgService.SendDelPkgToServer(pkgSend.Pid);

            return Ok();
        }
    }
}
