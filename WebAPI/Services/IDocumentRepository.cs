using System;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IDocumentRepository
    {
        List<Document> GetAll(string search, int PAGE_SIZE = 3, int page = 1);
        Document GetById(Guid id);
        Document Add(DocumentModel documentModel);
        void Update(Document document);
        void Delete(Guid id);
    }
}
