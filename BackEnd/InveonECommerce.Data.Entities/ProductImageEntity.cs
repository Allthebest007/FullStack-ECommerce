using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.Entities
{
    [Table("ProductImages")]
    public class ProductImageEntity : BaseEntity
    {
        public int ProductId { get; set; }

        public byte[] Image { get; set; }
        public bool IsDeleted { get; set; }


        public virtual ProductEntity Product{ get; set; }


    }
}
