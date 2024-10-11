using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class Degree
    {
        public int DegreeId { get; set; }
        public int Mark { get; set; }
        public DateTime AttemptDateTime { get; set; }

        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
