using InveonECommerce.Business.Engines.DTOs.User;
using InveonECommerce.Business.Engines.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InveonECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDto)
        {
            Response<UserDTO> response = await _userService.CreateUserAsync(createUserDto);
            return ActionResultInstance(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            Response<UserDTO> response = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            return ActionResultInstance(response);
        }

    }
}
