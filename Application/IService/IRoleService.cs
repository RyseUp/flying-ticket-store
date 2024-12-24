using Domain.Entities;

namespace Application.IService
{
    public interface IRoleService
    {
        Task<ICollection<Role>> GetAllRoles();
        Task<Role?> GetRoleByNameAsync(string roleName);
    }
}
