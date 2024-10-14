
namespace OnlineLearningPlatform.UI.ViewModels
{
    public class ApplicationUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateOnly? BirthDate { get; set; }
    }

}
