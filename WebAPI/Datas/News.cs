using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Datas
{
    public class News
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "ntext")]
        public string Content { get; set; }
        public string Image { get; set; }
        public string CreatedDate { get; set; }
        public Guid Category_News_id { get; set; }


        public Categories_News Categories_News { get; set; }

    }
}
