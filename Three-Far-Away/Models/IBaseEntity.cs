using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Far_Away.Models
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}
