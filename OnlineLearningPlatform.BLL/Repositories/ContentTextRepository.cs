using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.BLL.Repositories;
using OnlineLearningPlatform.DAL.Data;
using OnlineLearningPlatform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.BLL.Interfaces
{
    public class ContentTextRepository : BaseRepository<ContentText>, IContentTextRepository
    {
        private readonly DbContext _context;

        public ContentTextRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
