using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICategoryRepository
    {
        void CreateCategory(Category category);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackchanges);
        //Task<Category> GetCategoryAsync(Guid id, bool trackChanges);
        //void DeleteCategory(Category category);
    }
}
