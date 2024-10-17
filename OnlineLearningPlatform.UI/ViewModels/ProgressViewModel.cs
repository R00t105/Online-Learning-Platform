using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.UI.ViewModels
{
    public class ProgressViewModel
    {
            public List<Content> Contents { get; set; }
            public Content CurrentContent { get; set; }
            public List<ContentText> ContentTexts { get; set; }
            public int? NextContentId { get; set; }
            public int? PreviousContentId { get; set; }
            public int CourseId { get; set; }
    }
}
