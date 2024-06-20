using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mvcapp.Models.Team
{
    public class TeamViewModel
    {
        public Domain.Entities.Team Team { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public int SelectedUserId { get; set; }
        public int SelectedTeamId { get; set; }
    }
}
