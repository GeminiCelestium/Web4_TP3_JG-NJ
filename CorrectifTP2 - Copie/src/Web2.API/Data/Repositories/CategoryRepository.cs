using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(EventPlatformDbContext dbContext) : base(dbContext)
        {
        }

        public Category GetByIdIncludingEvents(int id)
        {
            return _dbContext.Categories.Include(c => c.Evenements).FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Category> GetList(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return new List<Category>();

            return _dbContext.Categories.Where(c => ids.Contains(c.ID)).AsEnumerable(); ;
        }

    }
}
