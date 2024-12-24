using Application.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository 
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public User? GetOneByEmailAndPassword(string email, string password)
        {
            return _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }
        
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
