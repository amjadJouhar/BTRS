using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class Passenger_BusTrip
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("PassengerID")]
        public Passenger passenger { get; set; }

        [ForeignKey("BusID")]
        public BusTrip bustrip { get; set; }
    }
}
