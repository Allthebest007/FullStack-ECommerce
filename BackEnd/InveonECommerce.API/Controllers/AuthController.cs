using InveonECommerce.Business.Engines.DTOs.Login;
using InveonECommerce.Business.Engines.DTOs.RefreshToken;
using InveonECommerce.Business.Engines.Interfaces.JwtInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InveonECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authService;
        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            return ActionResultInstance(result);
        }

        [HttpPost("revokeRefreshToken")]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDTO refreshToken)
        {
            var result = await _authService.RevokeRefreshTokenAsync(refreshToken.RefreshToken);
            return ActionResultInstance(result);
        }

        [HttpPost("createTokenByRefreshToken")]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDTO refreshToken)
        {
            var result = await _authService.CreateTokenByRefreshTokenAsync(refreshToken.RefreshToken);
            return ActionResultInstance(result);
        }


    }
}
