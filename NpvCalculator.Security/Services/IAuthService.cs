using NpvCalculator.Security.Classes;
using System;
using System.Threading.Tasks;

namespace NpvCalculator.Security.Services
{
    public interface IAuthService
    {
        Task<string> Login(Login login);
        Task<Guid> Register(Register register);
    }
}
