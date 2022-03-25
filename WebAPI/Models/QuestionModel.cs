using System;

namespace WebAPI.Models
{
    public class QuestionModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

    }
    public class Question : QuestionModel
    {
        public Guid Id { get; set; }
    }
}
