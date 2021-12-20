using InveonECommerce.Business.Engines.DTOs.Login;
using InveonECommerce.Business.Engines.DTOs.Token;
using InveonECommerce.Business.Engines.DTOs.User;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Interfaces.JwtInterfaces
{
    public interface IAuthenticationService
    {
        Task<Response<UserTokenDTO>> LoginAsync(LoginDTO loginDto);
        Task<Response<TokenDTO>> CreateTokenByRefreshTokenAsync(string refreshToken);

        // It can be used for when the user logout 
        Task<Response<NoDataDTO>> RevokeRefreshTokenAsync(string refreshToken);

        //When there is no authentication, use it
        Task<Response<ClientTokenDTO>> CreateTokenByClient(ClientLoginDTO clientLoginDto);



    }
}
