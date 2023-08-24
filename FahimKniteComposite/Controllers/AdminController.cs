using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FahimKniteComposite.Controllers
{
    public class AdminController : Controller
    {
       [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
