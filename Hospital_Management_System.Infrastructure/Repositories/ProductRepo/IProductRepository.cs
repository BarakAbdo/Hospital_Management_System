using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Infrastructure.Repositories.ProductRepo
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllCategory();
        Product GetByUId(string uid);
    }
}
