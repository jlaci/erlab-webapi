using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.Models
{
    public class QuestionFormDTO
    {
        public long Id { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public List<QuestionDTO> Questions { get; set; }

        // Because of the way JSON Deserialization works (first an object is created, then
        // its properties set), we need a default constructor.
        public QuestionFormDTO()
        {

        }

        public QuestionFormDTO(QuestionForm questionForm)
        {
            this.Id = questionForm.Id;
            this.Active = questionForm.Active;
            this.Name = questionForm.Name;
            this.Questions = questionForm
                                .Questions
                                .Select(q => new QuestionDTO(q))
                                .ToList();
        }

        public QuestionForm ToEntity()
        {
            return new QuestionForm()
            {
                Id = this.Id,
                Name = this.Name,
                Active = this.Active,
                Questions = this.Questions.Select(q => q.ToEntity()).ToList()
            };
        }
    }
}
