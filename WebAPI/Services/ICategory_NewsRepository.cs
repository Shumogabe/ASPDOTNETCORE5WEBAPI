﻿using System;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ICategory_NewsRepository
    {
        List<Category_News> GetAll();
        Category_News GetById(Guid id);
        Category_News Add(Category_NewsModel category_NewsModel);
        void Update(Category_News category_News);
        void Delete(Guid id);
    }
}
