using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.Entities
{
    [Table("Products")]
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public short UnitsInStock { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public bool EditorsFavorite { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }




        public virtual ICollection<OrderEntity> Orders { get; set; }
        public virtual CategoryEntity Category{ get; set; }
        public virtual ProductImageEntity ProductImage { get; set; }



    }
}
