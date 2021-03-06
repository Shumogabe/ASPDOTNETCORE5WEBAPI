using System;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ICategory_DocumentsRepository
    {
        List<Category_Documents> GetAll(string search, int PAGE_SIZE = 3, int page = 1);
        Category_Documents GetById(Guid id);
        Category_Documents Add(Category_DocumentsModel category_DocumentsModel);
        void Update(Category_Documents category_Documents);
        void Delete(Guid id);
    }
}
