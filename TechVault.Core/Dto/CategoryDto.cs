using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechVault.Core.Dto
{
    public record CategoryDto(string Name, string Description);
    public record UpdateCategoryDto(string Name, string Description,int Id);
}
