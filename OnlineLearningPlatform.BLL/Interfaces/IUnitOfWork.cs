using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserRepository ApplicationUsers { get; }
        IContentRepository Contents { get; }
        IContentTextRepository ContentTexts { get; }
        ICourseRepository Courses { get; }
        IEnrollmentRepository Enrollments { get; }
        IQuizRepository Quizzes { get; }
        IQuestionAnswerRepository QuestionAnswers { get; }
        IQuizQuestionRepository QuizQuestions { get; }
        IResultRepository Results { get; }
        ITrackRepository Tracks { get; }

        int Complete();
    }
}
