using OnlineLearningPlatform.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.UI.ViewModels
{
    public class CourseWithContentsViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }


        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Required]
        public int TrackId { get; set; }

        [Required]
        [MaxLength(25)]
        public string TrackName { get; set; }
        public List<Content>? contents { get; set; }
    }
}
