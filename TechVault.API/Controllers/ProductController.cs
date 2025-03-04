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
                var products = await _unitOfWork.ProductRepository
                    .GetAllAsync(p => p.Category, p => p.Photos);

                var result = _mapper.Map<List<ProductDto>>(products);

                if (result == null)
                {
                    return NotFound(new ResponseApi(400, "products not found"));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

            [HttpGet("get-by-id/{id}")]
            public async Task<IActionResult> GetAll(int id)
            {
                try
                {
                    var product = await _unitOfWork.ProductRepository
                        .GetByIdAsync(id, p => p.Category, p => p.Photos);

                    var result = _mapper.Map<ProductDto>(product);

                    if (result == null)
                    {
                        return NotFound(new ResponseApi(400, "product not found"));
                    }
                    return Ok(result);
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
    }
}
