using AutoMapper;
using InveonECommerce.Business.Engines.Constants.Messages;
using InveonECommerce.Business.Engines.DTOs.User;
using InveonECommerce.Business.Engines.Interfaces;
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

namespace InveonECommerce.Business.Engines.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IRepository<UserRefreshToken> _userRefreshTokenService;
        private readonly IUnitOfWork _uof;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<UserEntity> userManager,IMapper mapper, RoleManager<IdentityRole> roleManager, ITokenService tokenService, IUnitOfWork uof)
        {
            _uof = uof;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
            _userRefreshTokenService = _uof.GetRepository<UserRefreshToken>();
        }

        public async Task<Response<UserDTO>> CreateUserAsync(CreateUserDTO createUserDto)
        {
            //If the email is already exist
            var existUserWithEmail = await _userManager.FindByEmailAsync(createUserDto.Email);
            if(existUserWithEmail != null)
            {
                return Response<UserDTO>.Fail(UserMessages.UserEmailAlreadyExist, 400, true);
            }

            //Creating new User
            var user = new UserEntity
            {
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
            };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);


            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<UserDTO>.Fail(new ErrorDTO(errors, true), 400);
            }

            //If result is succeeded then add role to the user
            IdentityRole existRole = await _roleManager.FindByNameAsync(nameof(Role.Customer));
            await _userManager.AddToRoleAsync(user, existRole.Name);

            //Creating userDTO
            UserDTO userdto = _mapper.Map<UserDTO>(user);
            userdto.Role = nameof(Role.Customer);


            return Response<UserDTO>.Success(userdto, 200);

        }


        public async Task<Response<UserDTO>> GetUserByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                return Response<UserDTO>.Fail(UserMessages.UserNotFound, 404,true);
            }

            //Creating userDTO and adding role to userDTO
            UserDTO userdto = _mapper.Map<UserDTO>(user);
            IList<string> roleList = await _userManager.GetRolesAsync(user);
            string roleName = roleList.FirstOrDefault();
            userdto.Role = roleName;
            return Response<UserDTO>.Success(userdto, 200);
        }
    }
}
