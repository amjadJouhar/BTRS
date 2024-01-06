using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    
    [Index(nameof(Passenger.UserName), IsUnique = true)]
    [Index(nameof(Passenger.PhoneNumber), IsUnique = true)]
    [Index(nameof(Passenger.EmailAddress),IsUnique =true)]
    public class Passenger
    {
        
        [Key]
        public int PassengerID { get; set; }
        [Required(ErrorMessage ="Please Enter your Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter your Email")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please Enter a Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter your Gender")]
        public string Gender { get; set; }
		public enum gender
		{
			Male,
			Female
		}
		[Required(ErrorMessage = "Please Enter a UserName")]
        public string UserName { get; set; }
        public int PhoneNumber { get; set; }

        public ICollection<Passenger_BusTrip> passenger_BusTrip { get; set; }
    }
}
