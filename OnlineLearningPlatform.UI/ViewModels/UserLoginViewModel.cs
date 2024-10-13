using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineLearningPlatform.UI.ViewModels
{
    public class UserLoginViewModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Remmember Me")]
        [DefaultValue(false)]
        public bool RemmemberMe { get; set; }
    }
}
