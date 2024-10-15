using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.UI.ViewModels
{
    public class TrackViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }


        [Required]
        [MaxLength(150)]
        public string? Description { get; set; }
    }
}
