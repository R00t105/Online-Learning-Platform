using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class ApplicationUser:IdentityUser<int>
    {
        public DateTime? RegistrationDate { get; set; }
        public DateOnly? BirthDate { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();
    }
}
