using Application.Repositories;
using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;
using System;
using System.Linq;

namespace Infrastructure.Repositories
{
    public sealed class InterviewQuestionRepository : GenericRepository<InterviewQuestionModel, InterviewQuestion>, IInterviewQuestionRepository
    {
        private readonly MyDbContext _context;

        public InterviewQuestionRepository(MyDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
