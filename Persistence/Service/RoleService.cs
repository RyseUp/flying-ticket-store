using Application.IService;
using Application.IUnitOfWork;
using Domain.Entities;

namespace Persistence.Service
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<Role>> GetAllRoles()
        {
            return await _unitOfWork.Roles.GetAll();
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _unitOfWork.Roles.GetByNameAsync(roleName);
        }
    }
}
