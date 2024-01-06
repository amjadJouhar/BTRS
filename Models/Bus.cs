using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
    public class Bus
    {
        [Key]
        public int BusID { get; set; }
        [Required]
        public string CaptinName { get; set; }
		[Required]
		public int NumberOfSeats { get; set; }

        [ForeignKey("AdminID")]
        public Admin admin { get; set; }

        [ForeignKey("BusTripID")]
        public BusTrip bustrip { get; set; }
    }
}
