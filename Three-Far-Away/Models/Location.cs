using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Far_Away.Models
{
    [Table("locations")]
    public class Location : IBaseEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("address")]
        public String Address { get; set; }

        [Column("longitude")]
        public Double Longitude { get; set; }

        [Column("latitude")]
        public Double Latitude{ get; set; }

        public Location() { }
        public Location(Guid id,string address, double longitude, double latitude)
        {
            Id = id;
            Address = address;
            Longitude = longitude;
            Latitude = latitude;
        }
        public Location(string address, double longitude, double latitude)
        {
            Address = address;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
