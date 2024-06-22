using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcapp.Models;
using mvcapp.Models.Activities;

namespace mvcapp.Controllers
{
    [Authorize]
    public class ActivityController : BaseController
    {
        private readonly AppDbContext _context;

        public ActivityController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var user = await _context.Users.Include(x => x.Teams)
                                     .Include(x => x.OwnedTeams)
                                     .SingleOrDefaultAsync(x => x.Id == CurrentUserId);
            var types = await _context.ActivityTypes.ToListAsync();

            var activities = await _context.Activities.Include(x => x.Owner)
                                           .Include(x => x.Team)
                                           .Include(x => x.Type)
                                           .Where(x => user.Teams.Contains(x.Team))
                                           .ToListAsync();

            int pageSize = 3;
            var count = await _context.Activities.Where(x => user.Teams.Contains(x.Team)).CountAsync();

            activities = activities.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new ActivitiesViewModel
            {
                Teams = user.OwnedTeams,
                Types = types,
                AddViewModel = new AddActivityViewModel(),
                Activities = activities,
                PageViewModel = pageViewModel
            };

            return View(viewModel);
        }
    }
}
