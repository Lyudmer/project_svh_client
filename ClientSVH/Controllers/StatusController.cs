using ClientSVH.Application.Services;
using ClientSVH.Contracts;
using ClientSVH.Core.Abstraction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Linq;

namespace ClientSVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController(IStatusServices statusService) : ControllerBase
    {
        private readonly IStatusServices _statusService = statusService;

        [HttpPost("AddStatus")]
        public async Task<IActionResult> AddStatus([FromBody] StatusAddRequest statusRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           var result= await _statusService.AddStatus(statusRequest.Id, 
                                                      statusRequest.StatusName, statusRequest.RunWf, 
                                                      statusRequest.MkRes, statusRequest.SendMess);
            return Ok(result);
        }

        [HttpPost("DelStatus")]
        public async Task<IActionResult> DelStatus([FromBody] StatusDelRequest statusRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           var result = await _statusService.DelStatus(statusRequest.Id);
            return Ok(result);
        }
    }
}
