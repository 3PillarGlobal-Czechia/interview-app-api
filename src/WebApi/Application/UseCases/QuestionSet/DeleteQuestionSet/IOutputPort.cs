using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.DeleteQuestionSet
{
    public interface IOutputPort
    {
        void Invalid();

        void NotFound();

        void NoContent();
    }
}
