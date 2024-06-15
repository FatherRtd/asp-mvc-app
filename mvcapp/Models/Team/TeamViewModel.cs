namespace mvcapp.Models.Team
{
    public class TeamViewModel
    {
        public Domain.Entities.Team Team { get; set; }
        public bool CurrentUserIsOwner { get; set; }
    }
}
