using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IUserService
    {
        User? Authenticate(string username, string password);
        Task<bool> RegisterUserAsync(UserDTO userDto);
    }
}
