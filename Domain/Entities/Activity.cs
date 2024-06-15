using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Activity : Entity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int Mark	{ get; set; }

		public int OwnerId { get; set; }
		public int TeamId { get; set; }
		public int ActivityTypeId { get; set; }

		[ForeignKey(nameof(OwnerId))]
		public User Owner { get; set; }
		[ForeignKey(nameof(TeamId))]
		public Team Team { get; set; }
		[ForeignKey(nameof(ActivityTypeId))]
		public ActivityType Type { get; set; }
	}
}
