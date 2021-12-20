using InveonECommerce.Business.Engines.DTOs.AddressInfo;
using InveonECommerce.Business.Engines.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.DTOs.Order
{
    public class ListOrderDTO
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderedDate { get; set; }
        public decimal OrderTotal { get; set; }
        public UserDTO User { get; set; }
        public AddressInfoDTO AddressInfo { get; set; }
    }
}
