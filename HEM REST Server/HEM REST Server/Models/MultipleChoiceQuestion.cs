using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.Models
{
    public class MultipleChoiceQuestion : Question
    {

        public string SerializedPossibleAnswers { get; set; }

        // Entity Framework Core doesn't support Lists of primitives as values,
        // therefore we store the possible answers in a ; seperated list.
        [NotMapped]
        public List<string> PossibleAnswers {
            get
            {
                return SerializedPossibleAnswers.Split(";").ToList();
            }
            set
            {
                SerializedPossibleAnswers = String.Join(";", value);
            }
        }
    }
}
