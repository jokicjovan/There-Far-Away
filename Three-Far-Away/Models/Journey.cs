using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Three_Far_Away.Models
{
    [Table("journeys")]
    public class Journey : IBaseEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("name")]
        public String Name { get; set; }

        [Column("startDate")]
        public DateTime StartDate { get; set; }

        [Column("endDate")]
        public DateTime EndDate { get; set; }

        [Column("startLocation")]
        public Location? StartLocation { get; set; }

        [Column("endLocation")]
        public Location? EndLocation{ get; set; }

        [Column("price")]
        public Double Price { get; set; }

        [Column("attractions")]
        public List<Attraction> Attractions{ get; set; }

        [Column("transportation")]
        public TransportationType Transportation { get; set; }

        public Journey() { }

        public Journey(Guid id, string name, DateTime startDate, DateTime endDate, Location startLocation, Location endLocation, double price, List<Attraction> attractions, TransportationType transportation)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            StartLocation = startLocation;
            EndLocation = endLocation;
            Price = price;
            Attractions = attractions;
            Transportation = transportation;
        }
    }
}
