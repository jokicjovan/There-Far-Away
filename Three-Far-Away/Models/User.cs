using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Far_Away.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("name")]
        public String Name { get; set; }

        [Column("surname")]
        public String Surname { get; set; }

        public Role Role { get; set; }
        public User() { }
        public User(Guid id, string name, string surname, Role role)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Role = role;
        }
    }
}
