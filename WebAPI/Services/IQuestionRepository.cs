using System;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IQuestionRepository
    {
        List<Question> GetAll(string search, int PAGE_SIZE = 3, int page = 1);
        Question GetById(Guid id);
        Question Add(QuestionModel questionModel);
        void Update(Question question);
        void Delete(Guid id);
    }
}
