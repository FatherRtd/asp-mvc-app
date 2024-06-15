using System.ComponentModel.DataAnnotations;

namespace mvcapp.Models.Team
{
    public class TeamViewModel
    {
        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Name { get; set; }

        public IEnumerable<Domain.Entities.Team>? MyTeams { get; set; }
    }
}
