using Domain.Models.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class QuestionSetListItem
    {
        public QuestionSetModel QuestionSet { get; init; }
        public Difficulty Difficulty { get; set; }
    }
}
