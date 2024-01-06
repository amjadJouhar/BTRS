using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
	[Index(nameof(Admin.UserName), IsUnique = true)]
	public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        [Required]
        public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string FullName { get; set; }
        public ICollection<BusTrip> busTrip { get; set; }
        public ICollection<Bus> bus { get; set; }
    }
}
