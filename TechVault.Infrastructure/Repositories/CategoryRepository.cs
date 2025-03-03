using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechVault.Core.Entities.Product;
using TechVault.Core.Interfaces;
using TechVault.Infrastructure.Data;

namespace TechVault.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext _dbContext):base(_dbContext)
        {
            
        }
    }
}
