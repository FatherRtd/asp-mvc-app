using System.ComponentModel.DataAnnotations;

namespace mvcapp.Models.Team
{
    public class TeamsViewModel
    {
        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Name { get; set; }

        public bool CurrentUserIsOwner { get; set; }

        public IEnumerable<Domain.Entities.Team>? MyTeams { get; set; }
    }
}
