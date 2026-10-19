using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Application.Dtos.ProductsDtos
{
        public class CreateProductDto
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string? ImageUrl { get; set; }
            public int CategoryId { get; set; }
            public string UID { get; set; } = Guid.NewGuid().ToString();
        }

        public class UpdateProductDto : CreateProductDto
        {
            public int Id { get; set; }
        }

        public class ProductDto : UpdateProductDto
        {
            public string CategoryName { get; set; }
        }
 }

