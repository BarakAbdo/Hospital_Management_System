using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Application.Dtos.CategoriesDtos
{
    
    
        public class CreateCategoryDto
        {
            public string Name { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }

        public class UpdateCategoryDto : CreateCategoryDto
        {
            public int Id { get; set; }
        }

        public class CategoryDto : UpdateCategoryDto
        {
            
        }
    
}
