using Security.Core.Classes;
using System.Threading.Tasks;

namespace Security.Core.Services
{
    public interface IAuthService
    {
        Task<string> Login(Login login);
    }
}
