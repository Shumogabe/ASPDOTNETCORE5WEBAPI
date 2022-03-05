using System;

namespace WebAPI.Models
{
    public class Category_NewsModel
    {
        public string Title { get; set; }
    }
    public class Category_News : Category_NewsModel
    {
        public Guid Id { get; set; }
    }
}
