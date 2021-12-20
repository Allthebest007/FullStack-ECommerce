using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.DTOs.Token
{
    public class ClientTokenDTO
    {
        //This DTO is created for some client that don't need to authorization. Like weather forecast or foreign curreny sites...

        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }



    }
}
