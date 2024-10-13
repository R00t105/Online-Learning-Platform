using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.UI.ViewModels
{
    public class TrackViewModel
    {
        [Required]
        public string Name { get; set; }


        [Required]
        public string? Description { get; set; }
    }
}
