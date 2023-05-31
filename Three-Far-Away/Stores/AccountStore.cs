using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Stores
{
    public class AccountStore
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        private Role _role;
        public Role Role 
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
            }
        }
        public AccountStore() { }
    }
}
