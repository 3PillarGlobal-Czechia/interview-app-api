using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.DeleteQuestionSet
{
    public readonly struct DeleteQuestionSetInput
    {
        public int Id { get; init; }
        public IEnumerable<int> InterviewQuestionIds { get; init; }
    }
}
