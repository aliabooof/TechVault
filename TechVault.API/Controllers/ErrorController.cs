using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechVault.API.Helper;

namespace TechVault.API.Controllers
{
    [Route("errors/[statusCode]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new ResponseApi(statusCode));

        }
    }
}
