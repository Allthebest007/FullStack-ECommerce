using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.Entities
{
    [Table("Orders")]
    public class OrderEntity : BaseEntity
    {
        public OrderEntity()
        {
            
        }
        

        public string OrderNo { get; set; }
        public DateTime OrderedDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderTotal { get; set; }

        public int AddressInfoId { get; set; }
        public int BasketId { get; set; }
        public string UserId { get; set; }

        public virtual BasketEntity Basket { get; set; }
        public virtual AddressInfoEntity AddressInfo { get; set; }
        public virtual UserEntity User { get; set; }



    }
}
