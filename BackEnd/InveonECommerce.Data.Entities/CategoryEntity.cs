using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.Entities
{
    [Table("Categories")]
    public class CategoryEntity : BaseEntity 
    {
        public CategoryEntity()
        {
            Products = new List<ProductEntity>();
        }

        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ProductEntity> Products{ get; set; }

    }
}
