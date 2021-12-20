using InveonECommerce.Business.Engines.DTOs.AddressInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.DTOs.Order
{
    public class AddOrderDTO
    {
        public AddressInfoDTO AddressInfo { get; set; }
        public List<int> ItemsIds { get; set; }
        public string Username { get; set; }


    }
}
