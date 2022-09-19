using DatabaseAccess.DbWorker.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    [Authorize(Roles = ApplicationConstans.Roles.Administrator)]
    public class FisitkasSettingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FisitkasSettingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {

            return View(new FisitkasViewModel 
            { 
                Fisitkas = await _unitOfWork.Fisitkas.GetAll(), 
                Genres = await _unitOfWork.Genre.GetAll() 
            });
        }

        [HttpPost]
        public async Task<IActionResult> Save(Dictionary<string, string> fisitkas)
        {
            await _unitOfWork.Fisitkas.Update(fisitkas);
            await _unitOfWork.Save();
            return RedirectToActionPreserveMethod("Index", "Home");
        }
    }
}
