using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Infrastructure.Repositories.CategoryRepo
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetCategory(int id);
        Category GetByUId(string uid);
    }
}
