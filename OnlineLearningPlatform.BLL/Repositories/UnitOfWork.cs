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

        public IBaseRepository<ApplicationUser> ApplicationUsers { get; private set; }
        public IBaseRepository<Content> Contents { get; private set; }
        public IBaseRepository<ContentText> ContentTexts { get; private set; }
        public IBaseRepository<Course> Courses { get; private set; }
        public IBaseRepository<Enrollment> Enrollments { get; private set; }
        public IBaseRepository<Quiz> Quizzes { get; private set; }
        public IBaseRepository<QuestionAnswer> QuestionAnswers { get; private set; }
        public IBaseRepository<QuizQuestion> QuizQuestions { get; private set; }
        public IBaseRepository<Result> Results { get; private set; }
        public IBaseRepository<Track> Tracks { get; private set; }

        public UnitOfWork(AppDbContext Context)
        {
            _context = Context;

            ApplicationUsers = new BaseRepository<ApplicationUser>(_context);
            Contents = new BaseRepository<Content>(_context);
            ContentTexts = new BaseRepository<ContentText>(_context);
            Courses = new BaseRepository<Course>(_context);
            Enrollments = new BaseRepository<Enrollment>(_context);
            Quizzes = new BaseRepository<Quiz>(_context);
            QuestionAnswers = new BaseRepository<QuestionAnswer>(_context);
            QuizQuestions = new BaseRepository<QuizQuestion>(_context);
            Results = new BaseRepository<Result>(_context);
            Tracks = new BaseRepository<Track>(_context);
        }

        public int Complete() => 
            _context.SaveChanges();

        public void Dispose() =>
            _context.Dispose();
    }
}
