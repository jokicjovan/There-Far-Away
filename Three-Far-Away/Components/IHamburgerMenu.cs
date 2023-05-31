using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Far_Away.Components
{
    public interface IHamburgerMenu
    {
        double MenuWidth { get; set; }
        bool IsMenuOpen { get; set; }

    }
}
