using Domain.Entities;

namespace mvcapp.Models.Activities
{
    public class CompleteActivityViewModel
    {
        public Activity Activity { get; set; }

        public int ActivityId { get; set; }
        public int Mark { get; set; }
    }
}
