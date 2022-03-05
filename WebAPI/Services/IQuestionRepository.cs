using System;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IQuestionRepository
    {
        List<Question> GetAll();
        Question GetById(Guid id);
        Question Add(QuestionModel questionModel);
        void Update(Question question);
        void Delete(Guid id);
    }
}
