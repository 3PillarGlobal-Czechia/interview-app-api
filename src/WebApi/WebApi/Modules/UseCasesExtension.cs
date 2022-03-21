using Application.UseCases.InterviewQuestion.CreateInterviewQuestion;
using Application.UseCases.InterviewQuestion.GetInterviewQuestion;
using Application.UseCases.InterviewQuestion.UpdateInterviewQuestion;
using Application.UseCases.QuestionList.CreateQuestionList;
using Application.UseCases.QuestionList.GetQuestionList;
using Application.UseCases.QuestionList.UpdateQuestionList;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules;

public static class UseCasesExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services.AddTransient<IGetInterviewQuestionUseCase, GetInterviewQuestionUseCase>()
                       .AddTransient<ICreateInterviewQuestionUseCase, CreateInterviewQuestionUseCase>()
                       .AddTransient<IUpdateInterviewQuestionUseCase, UpdateInterviewQuestionUseCase>()
                       .AddTransient<ICreateQuestionListUseCase, CreateQuestionListUseCase>()
                       .AddTransient<IGetQuestionListUseCase, GetQuestionListUseCase>()
                       .AddTransient<IUpdateQuestionListUseCase, UpdateQuestionListUseCase>();
    }
}
