using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechVault.API.Helper;
using TechVault.Core.Dto;
using TechVault.Core.Entities.Product;
using TechVault.Core.Interfaces;
using TechVault.Infrastructure.Data.Migrations;

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

        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProduct(AddProductDto productDto)
        {
            try
            {
                await _unitOfWork.ProductRepository.AddAsync(productDto);
                return Ok(new ResponseApi(200));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Product")]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
        {
            try
            {
                await _unitOfWork.ProductRepository.UpdateAsync(updateProductDto);
                return Ok(new ResponseApi(200));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete-Product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(id,
                    x=>x.Photos,x=>x.Category);

                await _unitOfWork.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseApi(200));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
