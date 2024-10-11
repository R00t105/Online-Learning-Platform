using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public int TrackId { get; set; }
        public virtual Track Track { get; set; }
        public virtual ICollection<Content> Contents { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new HashSet<ApplicationUser>();

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();


    }
}
