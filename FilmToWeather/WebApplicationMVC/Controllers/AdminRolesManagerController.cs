using DatabaseAccess.DbWorker.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Models;
using static WebApplicationMVC.ApplicationConstans;

namespace WebApplicationMVC.Controllers
{
    [Authorize(Roles = ApplicationConstans.Roles.Administrator)]
    public class AdminRolesManagerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminRolesManagerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.AdminManager.GetRoles());
        }
        
        public async Task<IActionResult> Details(string roleName)
        {
            return View(new RoleDetailsViewModel
            {
                RoleName = roleName,
                UsersInRole = (await _unitOfWork.AdminManager.FindUsersInRole(roleName)).ToList()
            });
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(string roleName)
        {
            await _unitOfWork.AdminManager.CreateRole(roleName);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string roleName)
        {
            _unitOfWork.AdminManager.DeleteRole(roleName);
            return RedirectToAction("Index");
        }
    }
}
