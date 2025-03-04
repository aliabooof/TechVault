using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechVault.API.Helper;
using TechVault.Core.Dto;
using TechVault.Core.Interfaces;

namespace TechVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var product = await _unitOfWork.ProductRepository
                    .GetAllAsync(p=>p.Category, p=>p.Photos);

                var result = _mapper.Map<List<ProductDto>>(product);

                if (product == null)
                {
                    return NotFound(new ResponseApi(400,"product not found"));    
                }
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
