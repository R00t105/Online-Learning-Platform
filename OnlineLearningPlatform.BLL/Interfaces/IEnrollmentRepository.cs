﻿using OnlineLearningPlatform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.BLL.Interfaces
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment>
    {
        public Task<List<Course>> GetCoursesByUserIdAsync(int userId);
        public void Remove(int UserId,int courseId);
    }
}
