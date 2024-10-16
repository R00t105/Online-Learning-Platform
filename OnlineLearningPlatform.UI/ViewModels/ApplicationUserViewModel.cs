
using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.UI.ViewModels
{
    public class ApplicationUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime? RegistrationDate { get; set; }


        [DataType(DataType.Date)]
        public DateOnly? BirthDate { get; set; }


        [Display(Name="Role")]
        public int RoleId { get; set; }

        public List<string>? Roles { get; set; }
    }

}
