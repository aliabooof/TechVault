using AutoMapper;
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
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        
        
        public PhotoRepository(AppDbContext dbContext) : base(dbContext)
        {
            
           
        }

        
    }
}
