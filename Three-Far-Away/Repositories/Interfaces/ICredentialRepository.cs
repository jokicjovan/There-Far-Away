using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;

namespace Three_Far_Away.Repositories.Interfaces
{
    public interface ICredentialRepository : ICrudRepository<Credential>
    {
        Task<Credential> FindCredentialByUsername(string username);
    }
}
