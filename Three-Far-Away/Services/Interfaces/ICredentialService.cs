using System.Threading.Tasks;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;

namespace Three_Far_Away.Services.Interfaces
{
    public interface ICredentialService : ICrudService<Credential>
    {
        Task<User> Authenticate(string username, string password);
    }
}
