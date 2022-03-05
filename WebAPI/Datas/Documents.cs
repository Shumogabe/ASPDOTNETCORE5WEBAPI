using System;

namespace WebAPI.Datas
{
    public class Documents
    {
        public Guid Id { get; set; }
        public string Num_of_text { get; set; }
        public string Title { get; set; }    
        public string File { get; set; }
        public string CreatedDate { get; set; }
        public Guid Category_Documents_id { get; set; }


        public Categories_Documents Categories_Documents { get; set; }
    }
}
