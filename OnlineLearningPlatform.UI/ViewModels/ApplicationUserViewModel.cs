
using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.UI.ViewModels
{
    public class ApplicationUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime? RegistrationDate { get; set; }


        [DataType(DataType.Date)]
        public DateOnly? BirthDate { get; set; }
    }

}
