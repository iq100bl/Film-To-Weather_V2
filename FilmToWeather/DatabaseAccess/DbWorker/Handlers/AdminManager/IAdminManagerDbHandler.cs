using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace DatabaseAccess.DbWorker.Handlers.AdminManager
{
    public interface IAdminManagerDbHandler
    {
        Task<IdentityRole[]> GetRoles();
        Task<IdentityResult> CreateRole(string roleName);
        Task DeleteRole(string roleName);
        Task<User[]> GetUsers();
        Task<IList<string>> FindRolesUser(User user);
        Task UpdateUser(User user);
        Task ChangeRolesFromUserAsync(string userId, IEnumerable<string> roles);
        Task RemoveRolesAsync(User user, IEnumerable<string> removedRoles);
        Task<IList<User>> FindUsersInRole(string role);
        Task<User> GetUser(string userId);
    }
}
