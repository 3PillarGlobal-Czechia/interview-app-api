using Application.UseCases.InterviewQuestion.CreateInterviewQuestion;
using Application.UseCases.InterviewQuestion.GetInterviewQuestion;
using Application.UseCases.InterviewQuestion.UpdateInterviewQuestion;
using Application.UseCases.QuestionSet.GetQuestionSets;
using Microsoft.Extensions.DependencyInjection;
using Application.UseCases.QuestionSet.CreateQuestionSet;
using Application.UseCases.QuestionSet.GetQuestionSet;
using Application.UseCases.QuestionSet.UpdateQuestionSet;
using Application.UseCases.Question.DeleteInterviewQuestion;
using Application.UseCases.QuestionSet.UpdateQuestionOrder;

namespace WebApi.Modules;

public static class UseCasesExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services.AddTransient<IGetInterviewQuestionUseCase, GetInterviewQuestionUseCase>()
                       .AddTransient<ICreateQuestionUseCase, CreateQuestionUseCase>()
                       .AddTransient<IDeleteQuestionUseCase, DeleteQuestionUseCase>()
                       .AddTransient<IUpdateInterviewQuestionUseCase, UpdateInterviewQuestionUseCase>()
                       .AddTransient<ICreateQuestionSetUseCase, CreateQuestionSetUseCase>()
                       .AddTransient<IGetQuestionSetUseCase, GetQuestionSetUseCase>()
                       .AddTransient<IGetQuestionSetsUseCase, GetQuestionSetsUseCase>()     
                       .AddTransient<IUpdateQuestionSetUseCase, UpdateQuestionSetUseCase>()
                       .AddTransient<IUpdateQuestionOrderUseCase, UpdateQuestionOrderUseCase>();
    }
}
