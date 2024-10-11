using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class User:IdentityUser<int>
    {
        public DateTime RegistrationDate { get; set; }
    }
}
