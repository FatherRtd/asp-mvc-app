using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace mvcapp.Controllers
{
	public abstract class BaseController : Controller
	{
        protected int? CurrentUserId;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.UserName = User.Identity.IsAuthenticated ? User.Identity.Name : "";

            CurrentUserId = Convert.ToInt32(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier)
                                                .Select(x => x.Value)
                                                .SingleOrDefault());
        }
    }
}
