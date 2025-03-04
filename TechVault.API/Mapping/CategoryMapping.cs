using AutoMapper;
using TechVault.Core.Dto;
using TechVault.Core.Entities.Product;

namespace TechVault.API.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDto,Category>().ReverseMap();
            CreateMap<UpdateCategoryDto,Category>().ReverseMap();
        }
    }
}
