using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace mvcapp.Controllers
{
	public abstract class BaseController : Controller
	{
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.UserName = User.Identity.IsAuthenticated ? User.Identity.Name : "";
        }
    }
}
