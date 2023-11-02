using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.Data.Repositories
{
    public interface ICategoryRepository :IGenericRepository<Category>
    {
        public IEnumerable<Category> GetList(List<int> ids);
        public Category GetByIdIncludingEvents(int id);
    }
}
