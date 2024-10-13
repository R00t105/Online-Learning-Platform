using Microsoft.VisualBasic;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Data;
using OnlineLearningPlatform.DAL.Entities;
using System.Diagnostics;

namespace OnlineLearningPlatform.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IApplicationUserRepository ApplicationUsers { get; private set; }
        public IContentRepository Contents { get; private set; }
        public IContentTextRepository ContentTexts { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IEnrollmentRepository Enrollments { get; private set; }
        public IQuizRepository Quizzes { get; private set; }
        public IQuestionAnswerRepository QuestionAnswers { get; private set; }
        public IQuizQuestionRepository QuizQuestions { get; private set; }
        public IResultRepository Results { get; private set; }
        public ITrackRepository Tracks { get; private set; }

        public UnitOfWork(AppDbContext Context)
        {
            _context = Context;

            ApplicationUsers = new ApplicationUserRepository(_context);
            Contents = new ContentRepository(_context);
            ContentTexts = new ContentTextRepository(_context);
            Courses = new CourseRepository(_context);
            Enrollments = new EnrollmentRepository(_context);
            Quizzes = new QuizRepository(_context);
            QuestionAnswers = new QuestionAnswerRepository(_context);
            QuizQuestions = new QuizQuestionRepository(_context);
            Results = new ResultRepository(_context);
            Tracks = new TrackRepository(_context);
        }

        public int Complete() => 
            _context.SaveChanges();

        public void Dispose() =>
            _context.Dispose();
    }
}
