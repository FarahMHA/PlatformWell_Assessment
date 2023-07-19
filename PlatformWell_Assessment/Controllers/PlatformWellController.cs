using Microsoft.AspNetCore.Mvc;
using PlatformWell_Assessment.Models;
using PlatformWell_Assessment.Operations;

namespace PlatformWell_Assessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlatformWellController : ControllerBase
    {
        private readonly PlatformWellManagement _platformWellManagement;


        public PlatformWellController(PlatformWellManagement platformWellManagement)
        {
            _platformWellManagement = platformWellManagement;
        }


        [HttpGet("GetDataPlatformWell")]
        public async Task<string> GetDataPlatformWell()
        {
            var result = await _platformWellManagement.GetDataPlatformWell();
            return result;
        }
    }
}
