using InveonECommerce.Business.Engines.Configuration;
using InveonECommerce.Business.Engines.DTOs.Token;
using InveonECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Interfaces.JwtInterfaces
{
    public interface ITokenService
    {
        TokenDTO CreateToken(UserEntity user);
        ClientTokenDTO CreateTokenByClient(Client client);
    }
}
