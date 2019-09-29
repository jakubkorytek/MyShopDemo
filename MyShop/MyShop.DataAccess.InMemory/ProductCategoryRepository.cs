using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> categories;

        public ProductCategoryRepository()
        {
            categories = cache["categories"] as List<ProductCategory>;
            if (categories == null)
            {
                categories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["categories"] = categories;
        }

        public void Insert(ProductCategory category)
        {
            categories.Add(category);
        }

        public void Update(ProductCategory category)
        {
            ProductCategory categoryToUpdate = categories.Find(pc => pc.Id == category.Id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate = category;
            }
            else
            {
                throw new Exception("Category not found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory category = categories.Find(pc => pc.Id == Id);
            if (category != null)
            {
                return category;
            }
            else
            {
                throw new Exception("Category not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return categories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory categoryToDelete = categories.Find(pc => pc.Id == Id);
            if (categoryToDelete != null)
            {
                categories.Remove(categoryToDelete);
            }
            else
            {
                throw new Exception("Category not found");
            }
        }
    }
}
