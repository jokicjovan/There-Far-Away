using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Far_Away.Models
{
    [Table("attractions")]
    public class Attraction : IBaseEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("name")]
        public String Name { get; set; }

        [Column("description")]
        public String Description { get; set; }

        [Column("type")]
        public AttractionType Type { get; set; }

        [Column("image")]
        public String Image { get; set; }

        [Column("location")]
        public Location Location { get; set; }

        public Attraction() { }
        public Attraction(Guid id, string name, string description, AttractionType type, string image, Location location)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            Image = image;
            Location = location;
        }
    }
}
