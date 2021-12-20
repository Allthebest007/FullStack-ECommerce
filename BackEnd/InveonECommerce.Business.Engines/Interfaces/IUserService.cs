using InveonECommerce.Business.Engines.DTOs.User;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserDTO>> CreateUserAsync(CreateUserDTO createUserDto);
        Task<Response<UserDTO>> GetUserByNameAsync(string username);

    }
}
