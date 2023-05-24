using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Far_Away.Models
{
    [Table("credentials")]
    public class Credential : IBaseEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("username")]
        public String Username { get; set; }

        [Column("password")]
        public String Password { get; set; }

        public User User { get; set; }

        public Credential() { }
        public Credential(Guid id, string username, string password, User user)
        {
            Id = id;
            Username = username;
            Password = password;
            User = user;
        }
    }
}