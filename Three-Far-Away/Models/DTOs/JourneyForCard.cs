using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Three_Far_Away.Models.DTOs
{
    public class JourneyForCard
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Price { get; set; }

        public JourneyForCard(Guid id, string name, string date, string price)
        {
            Id = id;
            Name = name;
            Date = date;
            Price = price;
        }
        public JourneyForCard(Journey journey)
        {
            Id = journey.Id;
            Name = journey.Name;
            Date = journey.StartDate.ToString().Split(" ")[0] + " - " + journey.EndDate.ToString().Split(" ")[0];
            Price = journey.Price + " RSD";
        }

    }
}
