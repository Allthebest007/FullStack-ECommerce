using InveonECommerce.Business.Engines.DTOs.AddressInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.DTOs.Order
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderedDate { get; set; }
        public decimal OrderTotal { get; set; }
        public AddressInfoDTO AddressInfo { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }

    }
}
