using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
