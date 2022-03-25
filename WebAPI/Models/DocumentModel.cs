using Microsoft.AspNetCore.Http;
using System;

namespace WebAPI.Models
{
    public class DocumentModel
    {
        public string Num_of_text { get; set; }
        public string Title { get; set; }
        public string File { get; set; }
        public string CreatedDate { get; set; }
        public Guid Category_Documents_id { get; set; }
    }
    public class Document : DocumentModel
    {
        public Guid Id { get; set; }
    }
}
