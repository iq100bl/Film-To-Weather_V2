using DatabaseAccess.DbWorker.UnitOfWork;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    [Authorize(Roles = ApplicationConstans.Roles.Administrator)]
    public class AdminUsersManagerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminUsersManagerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.AdminManager.GetUsers());
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _unitOfWork.AdminManager.GetUser(id);
            var allRoles = await _unitOfWork.AdminManager.GetRoles();
            var userRoles = await _unitOfWork.AdminManager.FindRolesUser(user);
            UserDetailsViewModel model = new UserDetailsViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles.ToList(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, List<string> roles)
        {
            await _unitOfWork.AdminManager.ChangeRolesFromUserAsync(userId, roles);
            return RedirectToAction("Index");
        }
    }
}
