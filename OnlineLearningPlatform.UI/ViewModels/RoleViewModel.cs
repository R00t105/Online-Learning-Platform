using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.UI.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; } // Role ID

        [Required(ErrorMessage = "Role name is required.")]
        [Display(Name = "Role Name")]
       public string Name { get; set; } // Role name
    }
}
