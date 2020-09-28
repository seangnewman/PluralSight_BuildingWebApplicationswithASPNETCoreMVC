using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> AllCategories => new List<Category> {
           new  Category{ CategoryID = 1, CategoryName = "Fruit Pies", Description = "All-fruity, pies" },
           new  Category{ CategoryID = 2, CategoryName = "Cheese Cakes", Description = "Cheesy all the way" },
           new  Category{ CategoryID = 3, CategoryName = "Seasonal Pies", Description = "Get in the mood for a seasonal pie" }
        };
    }
}
