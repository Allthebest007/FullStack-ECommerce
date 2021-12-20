using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.Entities
{
    [Table("Baskets")]
    public class BasketEntity : BaseEntity
    {
        public BasketEntity()
        {
            BasketItems = new List<BasketItemEntity>();
        }
        public virtual ICollection<BasketItemEntity> BasketItems { get; set; }
        public virtual OrderEntity Order{ get; set; }
    }
}
