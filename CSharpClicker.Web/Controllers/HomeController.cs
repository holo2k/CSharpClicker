using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetName(string name)
        {
            return View(name);
        }
    }
}
