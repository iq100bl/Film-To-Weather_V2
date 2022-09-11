using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationMVC.Controllers
{
    [Authorize(Roles = ApplicationConstans.Roles.Administrator)]
    public class FisitkasSettingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
