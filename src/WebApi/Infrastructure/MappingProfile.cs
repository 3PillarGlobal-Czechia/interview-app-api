using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;

namespace Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<QuestionList, QuestionSetModel>().ReverseMap();
        CreateMap<InterviewQuestion, QuestionModel>().ReverseMap();
    }
}
