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
using Microsoft.AspNetCore.Http.HttpResults;


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
            int result=0;
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
                       result= await _pkgService.LoadFile(userId, resFile);
                    }
                }
            }
            return Ok(result);
        }
        [HttpPost("SendToServer")]
        public async Task<IActionResult> SendToServer(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
           var result= await _pkgService.SendToServer(pkgSend.Pid);

            return Ok(result);
        }
        [HttpPost("PkgFLK")]
        public async Task<IActionResult> PkgFLK(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _pkgService.PkgFLK(pkgSend.Pid);

            return Ok(result);
        }
        [HttpPost("LoadMessage")]
        public async Task<IActionResult> LoadMess()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _pkgService.LoadMessage();

            return Ok(result);
        }
        [HttpPost("GetHistory")]
        public async Task<IActionResult> GetHistoryPkg(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           var result= await _pkgService.HistoriPkgByPid(pkgSend.Pid);
            if (result == null)
                return BadRequest(ModelState);
            else
                return Ok(result);
        }
        [HttpPost("GetAllPackage")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _pkgService.GetAll();
            if (result == null)
                return BadRequest(ModelState);
            else
                return Ok(result);
        }
        [HttpPost("GetPackage")]
        public async Task<IActionResult> GetPkgId(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _pkgService.GetPkgId(pkgSend.Pid);
            if (result == null)
                return BadRequest(ModelState);
            else
                return Ok(result);
        } 
        [HttpPost("GetDocsPackage")]
        public async Task<IActionResult> GetDocsPkg(PackageRequest pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _pkgService.GetDocsPkg(pkgSend.Pid);
            if (result == null)
                return BadRequest(ModelState);
            else
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
