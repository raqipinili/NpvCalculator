using Security.Core.Classes;
using System;
using System.Threading.Tasks;

namespace Security.Core.Services
{
    public interface IUserService
    {
        Task<Guid> AddUser(Register register);
        Task<Guid> Register(Register register);
    }
}
