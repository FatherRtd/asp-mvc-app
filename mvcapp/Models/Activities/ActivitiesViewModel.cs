using Domain.Entities;

namespace mvcapp.Models.Activities
{
    public class ActivitiesViewModel
    {
        public List<Activity> Activities { get; set; }

        public List<ActivityType> Types { get; set; }

        public List<Domain.Entities.Team> Teams { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public AddActivityViewModel AddViewModel { get; set; }
    }
}
