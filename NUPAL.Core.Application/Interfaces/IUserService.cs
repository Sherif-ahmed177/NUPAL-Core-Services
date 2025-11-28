using Nupal.Domain.Entities;
using System.Collections.Generic;

namespace Nupal.Application.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
    }
}
