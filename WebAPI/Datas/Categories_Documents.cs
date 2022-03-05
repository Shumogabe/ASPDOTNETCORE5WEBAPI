using System;
using System.Collections.Generic;

namespace WebAPI.Datas
{
    public class Categories_Documents
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public ICollection<Documents> Documents { get; set; }
    }
}
