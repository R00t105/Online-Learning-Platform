using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.BLL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<ApplicationUser> ApplicationUsers { get; }
        IBaseRepository<Content> Contents { get; }
        IBaseRepository<ContentText> ContentTexts { get; }
        IBaseRepository<Course> Courses { get; }
        IBaseRepository<Enrollment> Enrollments { get; }
        IBaseRepository<Quiz> Quizzes { get; }
        IBaseRepository<QuestionAnswer> QuestionAnswers { get; }
        IBaseRepository<QuizQuestion> QuizQuestions { get; }
        IBaseRepository<Result> Results { get; }
        IBaseRepository<Track> Tracks { get; }

        int Complete();
    }
}
