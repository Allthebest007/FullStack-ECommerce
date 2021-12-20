using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.Entities
{

    [Table("BasketItems")]
    public class BasketItemEntity : BaseEntity
    {

        public int ProductId { get; set; }
        public int BasketId { get; set; }
        public int ProductQty { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }
        public virtual BasketEntity Basket { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}
