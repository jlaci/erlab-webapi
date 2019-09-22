using ElosztottLabor.Data;
using ElosztottLabor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.Interfaces
{
    public interface IQuestionFormService
    {
        IEnumerable<QuestionForm> GetQuestionForms();

        QuestionForm GetQuestionForm(long id);

        QuestionForm SaveQuestionForm(QuestionForm questionForm);

        void UpdateQuestionForm(long id, QuestionForm questionForm);

        void DeleteQuestionForm(long id);

        bool QuestionFormExistsById(long id);
    }
}
