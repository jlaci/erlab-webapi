using ElosztottLabor.Data;
using ElosztottLabor.Interfaces;
using ElosztottLabor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.Services
{
    public class QuestionFormService : IQuestionFormService
    {
        private readonly HEMDbContext _context;

        // Consturctor of the service, called by the framework. Notice that the argument
        // list is populated in runtime by the Dependency Injection (DI) solution.
        public QuestionFormService(HEMDbContext context)
        {
            this._context = context;

            var existing = _context.QuestionForms.Find(1L);
            if (existing == null)
            {
                // Add some mock data
                _context.QuestionForms.Add(new QuestionForm
                {
                    Id = 1,
                    Name = "Mock Question Form",
                    Active = true,
                    Questions = new List<Question>()
                    {
                        new MultipleChoiceQuestion()
                        {
                            QuestionText = "Melyik a kedvenc tanszéked?",
                            PossibleAnswers = new List<string>()
                            {
                                "AUT",
                                "Az automatizálási",
                                "Az Androidos"
                            }
                        }
                    }
                });
                _context.SaveChanges();
            }
        }

        public QuestionForm SaveQuestionForm(QuestionForm questionForm)
        {
            // If there is already a question form with this Id, thrown an exception
            if (QuestionFormExistsById(questionForm.Id))
            {
                throw new QuestionFormExistsException();
            }

            // Register the new question form as an entity tracekd by EF
            var result = _context.QuestionForms.Add(questionForm);

            // Save to database
            _context.SaveChanges();
            return result.Entity;
        }

        public void UpdateQuestionForm(long id, QuestionForm questionForm)
        {
            // If there are no question form with the id throw an expcetion
            if (!QuestionFormExistsById(id))
            {
                throw new QuestionFormDoesntExistsException();
            }

            var existing = _context
                .QuestionForms
                .Include(qf => qf.Questions)
                .SingleOrDefault(qf => qf.Id == id);
          
            // Remove the old questions
            foreach (var question in existing.Questions.ToList())
            {
                _context.Questions.Remove(question);
            }

            // Save the new date
            existing.Name = questionForm.Name;
            existing.Active = questionForm.Active;
            existing.Questions = questionForm.Questions;
            _context.SaveChanges();
        }

        public IEnumerable<QuestionForm> GetQuestionForms()
        {
            return _context
                .QuestionForms
                .Include(qf => qf.Questions)
                .ToList();
        }

        public QuestionForm GetQuestionForm(long id)
        {
            return _context
                .QuestionForms
                .Include(qf => qf.Questions)
                .SingleOrDefault(qf => qf.Id == id);
        }

        public void DeleteQuestionForm(long id)
        {
            var questionForm = _context
                .QuestionForms
                .Find(id);
            if (questionForm != null)
            {
                _context.Remove(questionForm);
                _context.SaveChanges();
            }
        }

        public bool QuestionFormExistsById(long id)
        {
            return _context
                .QuestionForms
                .Any(qf => qf.Id == id);
        }
    }
}
