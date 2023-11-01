using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public interface ICategoryBL
    {
        public IEnumerable<CategoryDTO> GetList();
        public CategoryDTO Get(int id);
        public CategoryDTO Add(CategoryDTO value);
        public CategoryDTO Updade(int id, CategoryDTO value);
        public void Delete(int id);
    }
}
