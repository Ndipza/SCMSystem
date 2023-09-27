using Microsoft.AspNetCore.Mvc;

namespace SCMSystem.Controllers
{
    public class SeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
