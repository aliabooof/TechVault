using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechVault.Core.Dto;
using TechVault.Core.Entities.Product;
using TechVault.Core.Interfaces;
using TechVault.Core.Services;
using TechVault.Infrastructure.Data;

namespace TechVault.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IImageManagementService _imageManagementService;
        private readonly IMapper _mapper;
        public ProductRepository(AppDbContext dbContext, IMapper mapper, IImageManagementService imageManagementService) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(AddProductDto addProductDto)
        {
            if (addProductDto == null)
                return false;
            var product = _mapper.Map<Product>(addProductDto);
            await _dbContext.Products.AddAsync(product);

            await _dbContext.SaveChangesAsync();

            var ImagePath = await _imageManagementService.AddImageAsync(addProductDto.Photos, addProductDto.Name);
            var photo = ImagePath.Select(path =>
            new Photo
            {
                ImageName = path,
                ProductId = product.Id,
            }).ToList();
            await _dbContext.Photos.AddRangeAsync(photo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(Product product)
        {
            var photo = await _dbContext.Photos.Where(m => m.ProductId == product.Id).ToListAsync();

            foreach (var item in photo)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(UpdateProductDto updateProductDto)
        {
            if (updateProductDto is null)
                return false;

            var findProduct = await _dbContext.Products.Include(m => m.Category)
                .Include(m => m.Photos)

                .FirstOrDefaultAsync(m => m.Id == updateProductDto.Id);
            if (findProduct is null)
                return false;

            _mapper.Map(updateProductDto, findProduct);

            if(updateProductDto.Photos is not null)
            {

                var findPhoto = await _dbContext.Photos.Where(m => m.ProductId == updateProductDto.Id).ToListAsync();

                foreach (var item in findPhoto)
                {
                    _imageManagementService.DeleteImageAsync(item.ImageName);
                }
                _dbContext.RemoveRange(findPhoto);
                var imagePath = await _imageManagementService.AddImageAsync(updateProductDto.Photos,updateProductDto.Name);

                var photo = imagePath.Select(path =>
                new Photo
                {
                    ImageName = path,
                    ProductId = updateProductDto.Id,
                }).ToList();

                await _dbContext.Photos.AddRangeAsync(photo);
            }
            await _dbContext.SaveChangesAsync();
            return true;

        }
    }
}
