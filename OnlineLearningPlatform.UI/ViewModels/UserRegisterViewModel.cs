using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.UI.ViewModels
{
    public class UserRegisterViewModel
    {
        public string Name { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
