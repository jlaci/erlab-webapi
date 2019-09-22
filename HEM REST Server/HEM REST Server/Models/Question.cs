using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.Models
{
    public abstract class Question
    {
        public long Id { get; set; }

        public string QuestionText { get; set; }
    }
}
