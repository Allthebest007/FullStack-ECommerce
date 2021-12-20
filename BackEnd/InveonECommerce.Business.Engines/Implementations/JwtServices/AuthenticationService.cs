using AutoMapper;
using InveonECommerce.Business.Engines.Constants.Messages;
using InveonECommerce.Business.Engines.DTOs.Login;
using InveonECommerce.Business.Engines.DTOs.Token;
using InveonECommerce.Business.Engines.DTOs.User;
using InveonECommerce.Business.Engines.Interfaces.JwtInterfaces;
using InveonECommerce.Data.DAL.Repository;
using InveonECommerce.Data.DAL.UnitOfWork;
using InveonECommerce.Data.Entities;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Implementations.JwtServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _uof;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IRepository<UserRefreshToken> _userRefreshTokenService;
        private readonly IMapper _mapper;

        public AuthenticationService(ITokenService tokenService,IUnitOfWork uof,UserManager<UserEntity> userManager,IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _uof = uof;
            _userManager = userManager;
            _userRefreshTokenService = _uof.GetRepository<UserRefreshToken>();
        }


        public async Task<Response<UserTokenDTO>> LoginAsync(LoginDTO loginDto)
        {
        
            if(loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }
            
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Response<UserTokenDTO>.Fail(AuthMessages.LoginInfosWrong, 400, true);
            }

            if ( !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Response<UserTokenDTO>.Fail(AuthMessages.LoginInfosWrong, 400, true);
            }

            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await _userRefreshTokenService.FirstOrDefaultAsync(x => x.UserId == user.Id);

            if( userRefreshToken == null)
            {
                await _userRefreshTokenService.AddAsync(new UserRefreshToken
                {
                    Code = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration,
                    UserId = user.Id
                });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _uof.CommitAsync();

            //Creating userTokenDTO and adding role to the user
            UserTokenDTO userTokenDto = new UserTokenDTO
            {
                User = _mapper.Map<UserDTO>(user),
                Token = token
            };
            IList<string> roleList = await _userManager.GetRolesAsync(user);
            string roleName = roleList.FirstOrDefault();
            userTokenDto.User.Role= roleName;

            return Response<UserTokenDTO>.Success(userTokenDto, 200);


        }

        public Task<Response<ClientTokenDTO>> CreateTokenByClient(ClientLoginDTO clientLoginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<TokenDTO>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var isExistRefreshToken = await _userRefreshTokenService.FirstOrDefaultAsync(x => x.Code == refreshToken);
            if (isExistRefreshToken == null)
            {
                return Response<TokenDTO>.Fail(TokenMessages.NotFoundRefreshToken, 404,true);
            }
            
            var user = await _userManager.FindByIdAsync(isExistRefreshToken.UserId);
            if (user == null)
            {
                return Response<TokenDTO>.Fail("Email or password is wrong", 400, true);
            }

            var tokenDto = _tokenService.CreateToken(user);
            isExistRefreshToken.Code = tokenDto.RefreshToken;
            isExistRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _uof.CommitAsync();

            return Response<TokenDTO>.Success(tokenDto,200);

        }

        public async Task<Response<NoDataDTO>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var isExistRefreshToken = await _userRefreshTokenService.FirstOrDefaultAsync(x => x.Code == refreshToken);
            if (isExistRefreshToken == null)
            {
                return Response<NoDataDTO>.Fail(TokenMessages.NotFoundRefreshToken, 404, true);
            }
            _userRefreshTokenService.Remove(isExistRefreshToken);
            await _uof.CommitAsync();
            return Response<NoDataDTO>.Success(200);
        }
    }
}
