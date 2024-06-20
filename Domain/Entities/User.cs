using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User : Entity
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }

	public List<Team> Teams { get; set; } = new();
	public List<Team> OwnedTeams = new();

	[NotMapped]
	public string FullName => $"{Name} {Surname}";
}