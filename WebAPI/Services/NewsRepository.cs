using System;
using System.Collections.Generic;
using WebAPI.Models;
using WebAPI.Datas;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace WebAPI.Services
{
    public class NewsRepository : INewsRepository
    {
        private readonly MyDbContext _context;
        //public static int PAGE_SIZE { get; set; } = 3;
        public NewsRepository(MyDbContext context)
        {
            _context = context;
        }
        public Models.News Add(NewsModel newsModel)
        {
            //using (WebClient client = new WebClient())
            //{
            //    client.DownloadFile(new Uri(url), @"c:\temp\image35.png");
            //    // OR 
            //    client.DownloadFileAsync(new Uri(url), @"c:\temp\image35.png");
            //}

            var news = new Datas.News
            {
                Title = newsModel.Title,
                Description = newsModel.Description,
                Content = newsModel.Content,
                Image = newsModel.Image,
                CreatedDate = newsModel.CreatedDate,
                Category_News_id = newsModel.Category_News_id,
            };
            _context.Add(news);
            _context.SaveChanges();
            return new Models.News
            {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                Content = news.Content,
                Image = news.Image,
                CreatedDate = news.CreatedDate,
                Category_News_id = news.Category_News_id,
            };
        }

        public void Delete(Guid id)
        {
            var news = _context.News.SingleOrDefault(x => x.Id == id);
            if (news != null)
            {
                _context.Remove(news);
                _context.SaveChanges();
            }
        }

        public List<Models.News> GetAll(string search, int PAGE_SIZE = 3, int page = 1)
        {


            var allProducts = _context.News.Include(i => i.Categories_News).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(x => x.Title.Contains(search) ||
                                                     x.Description.Contains(search) ||
                                                     x.Id.ToString().Contains(search) ||
                                                     x.Categories_News.Title.Contains(search) ||
                                                     x.CreatedDate.Contains(search));
            }

            var result = PaginatedList<Datas.News>.Create(allProducts, page, PAGE_SIZE);

            return result.Select(x => new Models.News
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Content = x.Content,
                Image = x.Image,
                CreatedDate = x.CreatedDate,
                Category_News_id = x.Category_News_id,
            }).ToList();
        }

        public Models.News GetById(Guid id)
        {
            var news = _context.News.SingleOrDefault(x => x.Id == id);
            if (news != null)
            {
                return new Models.News
                {
                    Id = news.Id,
                    Title = news.Title,
                    Description = news.Description,
                    Content = news.Content,
                    Image = news.Image,
                    CreatedDate = news.CreatedDate,
                    Category_News_id = news.Category_News_id,
                };
            }
            return null;
        }

        public void Update(Models.News news)
        {
            var _news = _context.News.SingleOrDefault(x => x.Id == news.Id);
            _news.Id = news.Id;
            _news.Title = news.Title;
            _news.Description = news.Description;
            _news.Content = news.Content;
            _news.Image = news.Image;
            _news.CreatedDate = news.CreatedDate;
            _news.Category_News_id = news.Category_News_id;
            _context.SaveChanges();
        }
        public static string ToVietNameseChacracter(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty);
        }
    }
}
