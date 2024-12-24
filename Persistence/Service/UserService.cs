using Application.DTOs;
using Application.IRepository;
using Application.IService;
using Application.IUnitOfWork;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public User? Authenticate(string username, string password)
        {
            return _unitOfWork.Users.GetOneByEmailAndPassword(username, password);
        }
        
        public async Task<bool> RegisterUserAsync(UserDTO userDto)
        {
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                return false;
            }

            var user = new User
            {
                Name = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password, 
                Phone = userDto.Phone,
                Address = userDto.Address,
                RoleId = userDto.RoleId
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
