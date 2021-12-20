using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.Entities
{
    [Table("AddressInfos")]
    public class AddressInfoEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }

        public virtual OrderEntity Order { get; set; }



    }
}
