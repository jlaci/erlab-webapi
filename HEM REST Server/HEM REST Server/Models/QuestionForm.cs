using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.Models
{
    public class QuestionForm
    {
        // By convention, this will be registered as the primary key
        // for explicit marking, you can use the [Key] annotation.
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }

        bool _active;
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                if (!_active && value)
                {
                    _active = true;
                }
            }
        }
    }
}
