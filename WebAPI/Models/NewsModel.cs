using Microsoft.AspNetCore.Http;
using System;

namespace WebAPI.Models
{
    public class NewsModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string CreatedDate { get; set; }
        public Guid Category_News_id { get; set; }
    }
    public class News : NewsModel
    {
        public Guid Id { get; set; }
    }
}
