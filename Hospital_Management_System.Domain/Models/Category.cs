using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString(); 
        public ICollection<Product> Products { get; set; }
    }
}
