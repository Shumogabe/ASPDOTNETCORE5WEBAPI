using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Datas;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly MyDbContext _context;

        //public static int PAGE_SIZE { get; set; } = 3;

        public DocumentRepository(MyDbContext context)
        {
            _context = context;
        }

        public Document Add(DocumentModel documentModel)
        {
            var document = new Documents
            {
                Num_of_text = documentModel.Num_of_text,
                Title = documentModel.Title,
                File = documentModel.File,
                CreatedDate = documentModel.CreatedDate,
                Category_Documents_id = documentModel.Category_Documents_id,
            };
            _context.Add(document);
            _context.SaveChanges();
            return new Document
            {
                Id = document.Id,
                Num_of_text = document.Num_of_text,
                Title = document.Title,
                File = document.File,
                CreatedDate = document.CreatedDate,
                Category_Documents_id = document.Category_Documents_id,
            };
        }

        public void Delete(Guid id)
        {
            var document = _context.Documents.SingleOrDefault(x => x.Id == id);
            if (document != null)
            {
                _context.Remove(document);
                _context.SaveChanges();
            }
        }

        public List<Document> GetAll(string search, int PAGE_SIZE = 3, int page = 1)
        {
            //var documents = _context.Documents.Select(x => new Document
            //{
            //    Id = x.Id,
            //    Num_of_text = x.Num_of_text,
            //    Title = x.Title,
            //    File = x.File,
            //    CreatedDate = x.CreatedDate,
            //    Category_Documents_id = x.Category_Documents_id,
            //});
            //return documents.ToList();

            var allProducts = _context.Documents.Include(i => i.Categories_Documents).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(x => x.Num_of_text.Contains(search) ||
                                                     x.Title.Contains(search) ||
                                                     x.Categories_Documents.Title.Contains(search) ||
                                                     x.CreatedDate.Contains(search));
            }

            var result = PaginatedList<Documents>.Create(allProducts, page, PAGE_SIZE);

            return result.Select(x => new Document
            {
                Id = x.Id,
                Num_of_text = x.Num_of_text,
                Title = x.Title,
                File = x.File,
                CreatedDate = x.CreatedDate,
                Category_Documents_id = x.Category_Documents_id,
            }).ToList();
        }

        public Document GetById(Guid id)
        {
            var document = _context.Documents.SingleOrDefault(x => x.Id == id);
            if (document != null)
            {
                return new Document
                {
                    Id = document.Id,
                    Num_of_text = document.Num_of_text,
                    Title = document.Title,
                    File = document.File,
                    CreatedDate = document.CreatedDate,
                    Category_Documents_id = document.Category_Documents_id,
                };
            }
            return null;
        }

        public void Update(Document document)
        {
            var _document = _context.Documents.SingleOrDefault(x => x.Id == document.Id);
            _document.Num_of_text = document.Num_of_text;
            _document.Title = document.Title;
            _document.File = document.File;
            _document.CreatedDate = document.CreatedDate;
            _document.Category_Documents_id = document.Category_Documents_id;
            _context.SaveChanges();
        }
    }
}
