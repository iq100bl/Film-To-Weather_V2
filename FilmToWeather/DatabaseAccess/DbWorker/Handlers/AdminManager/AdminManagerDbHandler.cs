using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess.DbWorker.Handlers.AdminManager
{
    public class AdminManagerDbHandler : IAdminManagerDbHandler
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminManagerDbHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<IdentityResult> CreateRole(string roleName)
        {
            return _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public Task DeleteRole(string roleName)
        {
            return _roleManager.DeleteAsync(_roleManager.Roles.Single(x => x.Name == roleName));
        }

        public Task<IList<string>> FindRolesUser(User user)
        {

            return _userManager.GetRolesAsync(user);
        }

        public Task<IdentityRole[]> GetRoles()
        {
            return _roleManager.Roles.ToArrayAsync();
        }

        public Task<User[]> GetUsers()
        {
            return _userManager.Users.ToArrayAsync();
        }

        public Task UpdateUser(User user)
        {
            return _userManager.UpdateAsync(user);
        }

        public async Task ChangeRolesFromUserAsync(string userId, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = roles.Except(userRoles).ToList();
            var removedRoles = userRoles.Except(roles).ToList();

            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
        }

        public Task RemoveRolesAsync(User user, IEnumerable<string> removedRoles)
        {
            return _userManager.RemoveFromRolesAsync(user, removedRoles);
        }

        public Task<IList<User>> FindUsersInRole(string role)
        {
            return _userManager.GetUsersInRoleAsync(role);
        }

        public Task<User> GetUser(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }
    }
}
