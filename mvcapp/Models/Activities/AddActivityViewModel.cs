using Domain.Entities;

namespace mvcapp.Models.Activities
{
    public class AddActivityViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ActivityType Type { get; set; }
    }
}
