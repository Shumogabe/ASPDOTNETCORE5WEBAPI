using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Datas
{
    public class Questions
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        [Column(TypeName = "ntext")] 
        public string Question { get; set; }
        [Column(TypeName = "ntext")]
        public string Answer { get; set; }
    }
}
