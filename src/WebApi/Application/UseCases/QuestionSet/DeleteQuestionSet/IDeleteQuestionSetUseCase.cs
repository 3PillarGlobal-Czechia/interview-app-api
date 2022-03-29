using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.DeleteQuestionSet
{
    public interface IDeleteQuestionSetUseCase
    {
        Task Execute(DeleteQuestionSetInput input);

        void SetOutputPort(IOutputPort outputPort);
    }
}
