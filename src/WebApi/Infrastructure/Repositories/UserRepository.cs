using Application.Repositories;
using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;
using System;
using System.Linq;

namespace Infrastructure.Repositories
{
    public sealed class UserRepository : GenericRepository<UserModel, User> , IUserRepository
    {
        private readonly MyDbContext _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(MyDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void GetByName(string name)
        {
            var user = _context.Users.Where(x => x.Name == name).ToList();
        }
    }
}
