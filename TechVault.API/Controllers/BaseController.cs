using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechVault.Core.Interfaces;

namespace TechVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }
    }
}
