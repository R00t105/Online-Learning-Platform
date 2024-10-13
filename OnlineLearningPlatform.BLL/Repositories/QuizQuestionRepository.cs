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
    public class QuizQuestionRepository : BaseRepository<QuizQuestion>, IQuizQuestionRepository
    {
        private readonly DbContext _context;

        public QuizQuestionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
