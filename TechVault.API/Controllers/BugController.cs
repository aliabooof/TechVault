using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechVault.Core.Interfaces;

namespace TechVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            return NotFound();
        }
        [HttpGet("server-error")]
        public async Task<ActionResult> ServerError()
        {
            return NotFound();
        }
        [HttpGet("bad-request")]
        public async Task<ActionResult> GetBadRequest()
        {
            return NotFound();
        }
    }
}
