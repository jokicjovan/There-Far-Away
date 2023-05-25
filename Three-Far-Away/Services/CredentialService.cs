using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;
using Three_Far_Away.Repositories.Interfaces;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.Services
{
    public class CredentialService : ICredentialService
    {
        private readonly ICredentialRepository _credentialRepository;

        public CredentialService(ICredentialRepository credentialRepository)
        {
            _credentialRepository = credentialRepository;
        }
        public Credential Create(Credential entity)
        {
            return _credentialRepository.Create(entity);
        }

        public Credential Read(Guid id)
        {
            return _credentialRepository.Read(id);
        }

        public Credential Update(Credential entity)
        {
            return _credentialRepository.Update(entity);
        }

        public Credential Delete(Guid id)
        {
            return _credentialRepository.Delete(id);
        }
    }
}
