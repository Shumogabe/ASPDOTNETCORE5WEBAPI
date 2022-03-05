using System;

namespace WebAPI.Models
{
    public class Category_DocumentsModel
    {
        public string Title { get; set; }
    }
    public class Category_Documents : Category_DocumentsModel
    {
        public Guid Id { get; set; }
    }
}
