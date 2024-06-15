using Microsoft.AspNetCore.Mvc;

namespace mvcapp.Controllers
{
    public class TeamController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
