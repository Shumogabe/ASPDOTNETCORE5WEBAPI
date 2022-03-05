using System;
using System.Collections.Generic;

namespace WebAPI.Datas
{
    public class Categories_News
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public ICollection<News> News { get; set; }
    }
}
