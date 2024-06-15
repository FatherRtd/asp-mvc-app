using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcapp.Models.Team;

namespace mvcapp.Controllers
{
    [Authorize]
    public class TeamController : BaseController
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(new TeamsViewModel
            {
                MyTeams = await GetUserTeams(),
            });
        }

        private async Task<IEnumerable<Team>> GetUserTeams()
        {
            var t = await _context.Teams.Include(x => x.Members)
                                  .Include(x => x.Owner)
                                  .Where(x => x.Members.Any(m => m.Id == CurrentUserId))
                                  .ToListAsync();

            t.ForEach(x => x.CurrentUserIsOwner = CurrentUserId == x.OwnerId);

            return t;
        }

        public async Task<IActionResult> CreateAsync(TeamsViewModel model)
        {
            if (!ModelState.IsValid || CurrentUserId == null)
            {
                model.MyTeams = await GetUserTeams();
                return View("Index", model);
            }

            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Name.ToLower() == model.Name.ToLower());
            if (team != null)
            {
                ViewBag.Error = "Команда с таким названием уже существует";
                model.MyTeams = await GetUserTeams();
                return View("Index", model);
            }

            var user = await _context.Users.SingleAsync(x => x.Id == CurrentUserId.Value);
            team = new Team
            {
                Name = model.Name,
                OwnerId = CurrentUserId.Value
            };
            team.Members.Add(user);

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            model.MyTeams = await GetUserTeams();

            return View("Index", model);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var team = await _context.Teams
                                     .Include(x => x.Members)
                                     .FirstOrDefaultAsync(x => x.Id == id);
            if (team == null)
            {
                ViewBag.DeleteTeamError = "Команды не существует";
                return View("Index", new TeamsViewModel
                {
                    MyTeams = await GetUserTeams()
                });
            }

            if (team.Members.Count() > 1)
            {
                ViewBag.DeleteTeamError = "В команде есть участники";
                return View("Index", new TeamsViewModel
                {
                    MyTeams = await GetUserTeams()
                });
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LeaveAsync(int id)
        {
            var team = await _context.Teams.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == id);
            if (team != null)
            {
                var currentUser = await _context.Users.Include(x => x.Teams).SingleAsync(x => x.Id == CurrentUserId);

                team.Members.Remove(currentUser);
                currentUser.Teams.Remove(team);

                _context.Teams.Update(team);
                _context.Users.Update(currentUser);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
