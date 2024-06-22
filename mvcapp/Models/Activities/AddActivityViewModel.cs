using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace mvcapp.Models.Activities
{
    public class AddActivityViewModel
    {
        [ValidateNever]
        public List<ActivityType> Types { get; set; }
        [ValidateNever]
        public List<Domain.Entities.Team> Teams { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Данное поле обязательно")]
        public int TypeId { get; set; }
        [Required(ErrorMessage = "Данное поле обязательно")]
        public int TeamId { get; set; }
    }
}
