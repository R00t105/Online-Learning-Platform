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
        
    }
}
