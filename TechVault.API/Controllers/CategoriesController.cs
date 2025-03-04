using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechVault.Core.Interfaces;

namespace TechVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            
        }

        [HttpGet("Get-all")]
        public async Task<IActionResult> get()
        {
            try { 
                var category = await _unitOfWork.CategoryRepository.GetAllAsync();

                if (category == null) 
                {
                    return BadRequest();
                }
                return Ok(category);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
