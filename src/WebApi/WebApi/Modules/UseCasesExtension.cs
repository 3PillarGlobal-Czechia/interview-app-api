using Application.UseCases.InterviewQuestion.CreateInterviewQuestion;
using Application.UseCases.InterviewQuestion.GetInterviewQuestion;
using Application.UseCases.InterviewQuestion.UpdateInterviewQuestion;
using Application.UseCases.QuestionSet.CreateQuestionList;
using Application.UseCases.QuestionSet.GetQuestionList;
using Application.UseCases.QuestionSet.UpdateQuestionList;
using Application.UseCases.QuestionSet.GetQuestionSets;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules;

public static class UseCasesExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services.AddTransient<IGetInterviewQuestionUseCase, GetInterviewQuestionUseCase>()
                       .AddTransient<ICreateInterviewQuestionUseCase, CreateInterviewQuestionUseCase>()
                       .AddTransient<IUpdateInterviewQuestionUseCase, UpdateInterviewQuestionUseCase>()
                       .AddTransient<ICreateQuestionSetUseCase, CreateQuestionSetUseCase>()
                       .AddTransient<IGetQuestionSetUseCase, GetQuestionSetUseCase>()
                       .AddTransient<IGetQuestionSetsUseCase, GetQuestionSetsUseCase>()     
                       .AddTransient<IUpdateQuestionSetUseCase, UpdateQuestionSetUseCase>();
    }
}
