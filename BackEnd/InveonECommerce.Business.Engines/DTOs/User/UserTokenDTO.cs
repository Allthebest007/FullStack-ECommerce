using InveonECommerce.Business.Engines.DTOs.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.DTOs.User
{
    public class UserTokenDTO
    {
        public UserDTO User { get; set; }
        public TokenDTO Token{ get; set; }
    }
}
