using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechVault.Core.Interfaces;
using TechVault.Core.Services;
using TechVault.Infrastructure.Data;

namespace TechVault.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;

        public ICategoryRepository CategoryRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public IProductRepository ProductRepository { get; }

        public UnitOfWork(AppDbContext dbContext, IMapper mapper, IImageManagementService imageManagementService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _imageManagementService = imageManagementService;

            CategoryRepository = new CategoryRepository(_dbContext);
            PhotoRepository = new PhotoRepository(_dbContext);
            ProductRepository = new ProductRepository(_dbContext, _mapper, _imageManagementService);
        }
    }
}
