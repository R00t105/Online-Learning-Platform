using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class ApplicationUser:IdentityUser<int>
    {
        public DateTime RegistrationDate { get; set; }
        public DateOnly BirthDate { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();

    }
}
