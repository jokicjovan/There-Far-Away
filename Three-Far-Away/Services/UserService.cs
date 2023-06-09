﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;
using Three_Far_Away.Repositories.Interfaces;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region CRUD
        public User Create(User entity)
        {
            return _userRepository.Create(entity);
        }

        public User Read(Guid id)
        {
            return _userRepository.Read(id);
        }

        public List<User> ReadAll()
        {
            return (List<User>)_userRepository.ReadAll();
        }

        public User Update(User entity)
        {
            return _userRepository.Update(entity);
        }

        public User Delete(Guid id)
        {
            return _userRepository.Delete(id);
        }
        #endregion

    }
}
