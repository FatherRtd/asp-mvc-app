using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using mvcapp.Models;
using mvcapp.Models.Activities;
using mvcapp.Models.Users;

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

            activities = activities.OrderByDescending(x => x.StartDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new ActivitiesViewModel
            {
                AddViewModel = new AddActivityViewModel{Types = types, Teams = user.Teams},
                Activities = activities,
                PageViewModel = pageViewModel
            };

            return View(viewModel);
        }

        public IActionResult ById(int id)
        {
            return View("Activity");
        }

        public async Task<IActionResult> Add(AddActivityViewModel model)
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

            var count = await _context.Activities.Where(x => user.Teams.Contains(x.Team)).CountAsync();
            activities = activities.Take(3).ToList();
            var pageViewModel = new PageViewModel(count, 1, 3);

            if (!ModelState.IsValid)
            {
                model.Types = types;
                model.Teams = user.Teams;
                var viewModel = new ActivitiesViewModel
                {
                    AddViewModel = model,
                    Activities = activities,
                    PageViewModel = pageViewModel
                };
                return View("Index", viewModel);
            }

            var team = await _context.Teams.SingleOrDefaultAsync(x => x.Id == model.TeamId);
            var activity = new Activity
            {
                Team = team,
                Name = model.Name,
                Description = model.Description,
                StartDate = DateTime.UtcNow,
                Mark = 0,
                Owner = user,
                Type = types.SingleOrDefault(x => x.Id == model.TypeId)
            };

            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
