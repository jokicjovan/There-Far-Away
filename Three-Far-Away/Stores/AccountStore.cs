using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public AccountStore(
            ) { }
    }
}
