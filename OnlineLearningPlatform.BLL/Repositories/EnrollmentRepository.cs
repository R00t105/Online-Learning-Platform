using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.DAL.Data;
using OnlineLearningPlatform.DAL.Entities;
using OnlineLearningPlatform.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.BLL.Interfaces
{
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
    {
        private readonly DbContext _context;
        private readonly AppDbContext appContext;

        public EnrollmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
            this.appContext = context;
        }
        public async Task<List<Course>> GetCoursesByUserIdAsync(int userId)
        {
            var x = await appContext.Enrollments
        .Where(e => e.ApplicationUserId == userId)
        .Include(e => e.Course) // Include related Course entity
        .Select(e => e.Course) // Select only the Course
        .ToListAsync();

            return x;
        }
        public void Remove(int UserId, int courseId)
        {
            Enrollment e = appContext.Enrollments.FirstOrDefault(i => i.ApplicationUser.Id == UserId && i.CourseId == courseId);
            appContext.Enrollments.Remove(e);
            appContext.SaveChanges();
        }
        public async Task<Enrollment> GetEnrollby2keysasync(int UserId, int courseId)
        {
            Enrollment e = appContext.Enrollments.FirstOrDefault(i => i.ApplicationUserId == UserId && i.CourseId == courseId);
            return e;
        }

    }
}
