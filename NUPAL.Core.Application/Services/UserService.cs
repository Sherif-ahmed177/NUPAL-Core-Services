using Nupal.Application.Interfaces;
using Nupal.Domain.Entities;
using Nupal.Infrastructure.Repositories;
using System.Collections.Generic;

namespace Nupal.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<User> GetAllUsers() => _repo.GetAll();
        public User GetUserById(int id) => _repo.GetById(id);
    }
}
