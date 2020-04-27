using Howru.Data;
using System.Threading.Tasks;

namespace Howru.Auth.Interfaces
{
    public interface IJwtGenerator
    {
        Task<object> GenerateJwt(User user);
    }
}
