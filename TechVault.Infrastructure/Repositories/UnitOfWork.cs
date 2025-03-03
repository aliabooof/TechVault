﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechVault.Core.Interfaces;
using TechVault.Infrastructure.Data;

namespace TechVault.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public ICategoryRepository CategoryRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public IProductRepository ProductRepository { get; }

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            CategoryRepository = new CategoryRepository(_dbContext); 
            PhotoRepository = new PhotoRepository(_dbContext);
            ProductRepository = new ProductRepository(_dbContext);
        }
    }
}
