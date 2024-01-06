using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
	[Index(nameof(BusTrip.BusNumber), IsUnique = true)]
	public class BusTrip
    {
        [Key]
        public int BusTripID { get; set; }
        [Required]
        public string TripDistination { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int BusNumber { get; set; }

        public ICollection<Passenger_BusTrip> passenger_BusTrip { get; set; }
        public ICollection<Bus> bus { get; set; }

        [ForeignKey("AdminID")]
        public Admin admin { get; set; }

        

    }
}
