using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.Models
{
    public enum QuestionType { FreeText, MultipleChoice, TrueOrFalse }

    public class QuestionDTO
    {
        public long Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionText { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public int MaxAnswerLength { get; set; }

        // Because of the way JSON Deserialization works (first an object is created, then 
        // its properties set), we need a default constructor.
        public QuestionDTO()
        {

        }

        public QuestionDTO(Question question)
        {
            this.Id = question.Id;
            this.QuestionText = question.QuestionText;
            
            if (question is MultipleChoiceQuestion)
            {
                MapMultipleChoice(question as MultipleChoiceQuestion);
            }
            else if (question is TrueOrFalseQuestion)
            {
                MapTrueOrFalse(question as TrueOrFalseQuestion);
            }
            else if (question is FreeTextQuestion)
            {
                MapFreeTextQuestion(question as FreeTextQuestion);
            }
        }

        public Question ToEntity()
        {
            switch(QuestionType)
            {
                case QuestionType.FreeText:
                    return new FreeTextQuestion()
                    {
                        Id = this.Id,
                        QuestionText = this.QuestionText,
                        MaxAnswerLength = this.MaxAnswerLength
                    };
                case QuestionType.MultipleChoice:
                    return new MultipleChoiceQuestion()
                    {
                        Id = this.Id,
                        QuestionText = this.QuestionText,
                        PossibleAnswers = this.PossibleAnswers
                    };
                case QuestionType.TrueOrFalse:
                    return new TrueOrFalseQuestion()
                    {
                        Id = this.Id,
                        QuestionText = this.QuestionText
                    };
                default:
                    throw new NotImplementedException();
            }
        }

        private void MapMultipleChoice(MultipleChoiceQuestion question)
        {
            this.QuestionType = QuestionType.MultipleChoice;
            this.PossibleAnswers = question.PossibleAnswers;
            this.MaxAnswerLength = 1;
        }

        private void MapTrueOrFalse(TrueOrFalseQuestion trueOrFalseQuestion)
        {
            this.QuestionType = QuestionType.TrueOrFalse;
            this.PossibleAnswers = new List<string>() { "true", "false" };
            this.MaxAnswerLength = 1;
        }

        private void MapFreeTextQuestion(FreeTextQuestion question)
        {
            this.QuestionType = QuestionType.FreeText;
            this.MaxAnswerLength = question.MaxAnswerLength;
        }
    }
}
