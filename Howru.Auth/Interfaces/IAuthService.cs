using Howru.Data.Dto;
using System.Threading.Tasks;

namespace Howru.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<object> Login(string userName, string password);
        Task<object> Register(UserDto item);
    }
}
