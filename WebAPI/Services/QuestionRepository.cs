using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Datas;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly MyDbContext _context;

        public QuestionRepository(MyDbContext context)
        {
            _context = context;
        }
        public Question Add(QuestionModel questionModel)
        {
            var question = new Questions
            {
                Name = questionModel.Name,
                Email = questionModel.Email,
                Title = questionModel.Title,
                Question = questionModel.Question,
            };
            _context.Add(question);
            _context.SaveChanges();
            return new Question
            {
                Id = question.Id,
                Name = question.Name,
                Email = question.Email,
                Title = question.Title,
                Question = question.Question,
            };
        }

        public void Delete(Guid id)
        {
            var question = _context.Questions.SingleOrDefault(x => x.Id == id);
            if (question != null)
            {
                _context.Remove(question);
                _context.SaveChanges();
            }
        }

        public List<Question> GetAll(string search, int PAGE_SIZE = 3, int page = 1)
        {
            //var questions = _context.Questions.Select(x => new Question
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Email = x.Email,
            //    Title = x.Title,
            //    Question = x.Question,
            //});
            //return questions.ToList();

            var allProducts = _context.Questions.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(x => x.Name.Contains(search) ||
                                                     x.Email.Contains(search) ||
                                                     x.Title.Contains(search) ||
                                                     x.Question.Contains(search));
            }

            var result = PaginatedList<Questions>.Create(allProducts, page, PAGE_SIZE);

            return result.Select(x => new Question
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Title = x.Title,
                Question = x.Question,
                Answer = x.Answer,
            }).ToList();
        }

        public Question GetById(Guid id)
        {
            var question = _context.Questions.SingleOrDefault(x => x.Id == id);
            if (question != null)
            {
                return new Question
                {
                    Id = question.Id,
                    Name = question.Name,
                    Email = question.Email,
                    Title = question.Title,
                    Question = question.Question,
                    Answer = question.Answer,
                };
            }
            return null;
        }

        public void Update(Question question)
        {
            var _question = _context.Questions.SingleOrDefault(x => x.Id == question.Id);
            _question.Name = question.Name;
            _question.Email = question.Email;
            _question.Title = question.Title;
            _question.Question = question.Question;
            _question.Answer = question.Answer;
            _context.SaveChanges();
        }
    }
}
