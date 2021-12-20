using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.DTOs.Product
{
    public class UpdateProductDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short UnitsInStock { get; set; }
        public decimal Price { get; set; }
        public bool EditorsFavorite { get; set; }
        public ProductImageDTO ProductImage { get; set; }
        public int CategoryId { get; set; }

        public class ProductImageDTO
        {
            public int Id { get; set; }
            public byte[] Image { get; set; }
        }
    }
}
