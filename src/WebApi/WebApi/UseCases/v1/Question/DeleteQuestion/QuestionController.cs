using Application.UseCases.Question.DeleteInterviewQuestion;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.Question.DeleteQuestion
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase, IOutputPort
    {
        private IActionResult _viewModel;

        private readonly IDeleteQuestionUseCase _useCase;

        public QuestionController(IDeleteQuestionUseCase deleteInterviewQuestion)
        {
            _useCase = deleteInterviewQuestion;
        }

        void IOutputPort.Invalid()
        {
            _viewModel = BadRequest();
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        void IOutputPort.NoContent()
        {
            _viewModel = NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteQuestion")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var input = new DeleteQuestionInput
            {
                Id = id
            };

            _useCase.SetOutputPort(this);

            await _useCase.Execute(input);

            return _viewModel;
        }
    }
}
