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


        [HttpGet("GetDataPlatformWellActual")]
        public async Task<string> GetDataPlatformWellActual()
        {
            var result = await _platformWellManagement.GetDataPlatformWellActual();
            return result;
        }

        [HttpGet("GetDataPlatformWellDummy")]
        public async Task<string> GetDataPlatformWellDummy()
        {
            var result = await _platformWellManagement.GetDataPlatformWellDummy();
            return result;
        }
    }
}
