using System;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface INewsRepository
    {
        List<News> GetAll(string search, int PAGE_SIZE = 3, int page = 1);
        News GetById(Guid id);
        //News GetByCategory(Guid id);
        News Add(NewsModel newsModel);
        void Update(News news);
        void Delete(Guid id);
        
    }
}
