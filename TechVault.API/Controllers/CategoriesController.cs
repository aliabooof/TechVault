using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechVault.API.Helper;
using TechVault.Core.Dto;
using TechVault.Core.Entities.Product;
using TechVault.Core.Interfaces;

namespace TechVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper):base(unitOfWork, mapper)
        {
            
        }

        [HttpGet("Get-all")]
        public async Task<IActionResult> get()
        {
            try { 
                var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

                if (categories == null) 
                {
                    return BadRequest(new ResponseApi(400,$"not Categories found"));
                }
                return Ok(categories);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound(new ResponseApi(400, $"not found category by Id{id}"));
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);

                await _unitOfWork.CategoryRepository.AddAsync(category);
                
                return Ok(new ResponseApi(200, "item has been added"));
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                await _unitOfWork.CategoryRepository.UpdateAsync(category);
                return Ok(new ResponseApi(200, "item has been updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _unitOfWork.CategoryRepository.DeleteAsync(id);
                return Ok(new ResponseApi(200, "item has been deleted"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
