using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Team : Entity
	{
		public string Name { get; set; }

		public int OwnerId { get; set; }

		public User Owner { get; set; }

		public List<User> Members { get; set; } = new();
	}
}
