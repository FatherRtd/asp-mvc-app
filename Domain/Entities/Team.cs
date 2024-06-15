using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Team : Entity
	{
		public string Name { get; set; }

		public int OwnerId { get; set; }

		[ForeignKey(nameof(OwnerId))]
		public User Owner { get; set; }

		public List<User> Members { get; set; } = new();

		[NotMapped]
		public bool CurrentUserIsOwner { get; set; } = false;
	}
}
