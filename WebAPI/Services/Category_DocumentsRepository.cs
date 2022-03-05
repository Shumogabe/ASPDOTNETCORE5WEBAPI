using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Datas;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class Category_DocumentsRepository : ICategory_DocumentsRepository
    {
        private readonly MyDbContext _context;

        public Category_DocumentsRepository(MyDbContext context)
        {
            _context = context;
        }

        public Category_Documents Add(Category_DocumentsModel category_DocumentsModel)
        {
            var category_Documents = new Categories_Documents
            {
                Title = category_DocumentsModel.Title,
            };
            _context.Add(category_Documents);
            _context.SaveChanges();
            return new Category_Documents
            {
                Id = category_Documents.Id,
                Title = category_DocumentsModel.Title,
            };
        }

        public void Delete(Guid id)
        {
            var category_Documents = _context.Categories_Documents.SingleOrDefault(x => x.Id == id);
            if (category_Documents != null)
            {
                _context.Remove(category_Documents);
                _context.SaveChanges();
            }
        }

        public List<Category_Documents> GetAll()
        {
            var categories_Documents = _context.Categories_Documents.Select(x => new Category_Documents
            {
                Id = x.Id,
                Title = x.Title,
            });
            return categories_Documents.ToList();
        }

        public Category_Documents GetById(Guid id)
        {
            var category_Documents = _context.Categories_Documents.SingleOrDefault(x => x.Id == id);
            if (category_Documents != null)
            {
                return new Category_Documents
                {
                    Id = category_Documents.Id,
                    Title = category_Documents.Title
                };
            }
            return null;
        }

        public void Update(Category_Documents category_Documents)
        {
            var _category_Documents = _context.Categories_Documents.SingleOrDefault(x => x.Id == category_Documents.Id);
            _category_Documents.Title = category_Documents.Title;
            _context.SaveChanges();
        }
    }
}
