using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Datas;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class Category_NewsRepository : ICategory_NewsRepository
    {
        private readonly MyDbContext _context;

        public Category_NewsRepository(MyDbContext context)
        {
            _context = context;
        }

        public Category_News Add(Category_NewsModel category_NewsModel)
        {
            var category_News = new Categories_News
            {
                Title = category_NewsModel.Title,
            };
            _context.Add(category_News);
            _context.SaveChanges();
            return new Category_News
            {
                Id = category_News.Id,
                Title = category_News.Title
            };
        }

        public void Delete(Guid id)
        {
            var category_News = _context.Categories_News.SingleOrDefault(c => c.Id == id);
            if (category_News != null)
            {
                _context.Remove(category_News);
                _context.SaveChanges();
            }
        }

        public List<Category_News> GetAll(string search, int PAGE_SIZE = 3, int page = 1)
        {
            //var categories_News = _context.Categories_News.Select(c => new Category_News
            //{
            //    Id = c.Id,
            //    Title = c.Title,
            //});
            //return categories_News.ToList();

            var allProducts = _context.Categories_News.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(x => x.Title.Contains(search));
            }

            var result = PaginatedList<Categories_News>.Create(allProducts, page, PAGE_SIZE);

            return result.Select(x => new Category_News
            {
                Id = x.Id,
                Title = x.Title,
            }).ToList();
        }

        public Category_News GetById(Guid id)
        {
            var category_News = _context.Categories_News.SingleOrDefault(lo => lo.Id == id);
            if (category_News != null)
            {
                return new Category_News
                {
                    Id = category_News.Id,
                    Title = category_News.Title
                };
            }
            return null;
        }

        public void Update(Category_News category_News)
        {
            var _category_News = _context.Categories_News.SingleOrDefault(lo => lo.Id == category_News.Id);
            _category_News.Title = category_News.Title;
            _context.SaveChanges();
        }
    }
}
