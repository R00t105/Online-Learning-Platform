using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.UI.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public int TrackId { get; set; }
        //List<Track> Tracks { get; set; }
    }
}
