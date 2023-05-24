using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Far_Away.Models
{
    [Table("arrangements")]
    public class Arrangement : IBaseEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("status")]
        public ArrangementStatus Status { get; set; }

        [Column("user")]
        public User User { get; set; }

        [Column("journey")]
        public Journey Journey { get; set; }

        public Arrangement() { }

        public Arrangement(Guid id, ArrangementStatus status, User user, Journey journey)
        {
            Id = id;
            Status = status;
            User = user;
            Journey = journey;
        }
    }
}
