using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class Enrollment
    {
        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public int EnrollmentId { get; set; }

        public DateTime? EnrolmentDate { get; set; }

        public int ProgressId { get; set; }

        public string ProgressState { get; set; }

        public DateTime? CompilationDate { get; set; }

        public int ProgressPercentage { get; set; }


    }
}
