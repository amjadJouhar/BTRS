using BTRS.Data;
using BTRS.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace BTRS.Controllers
{
    public class PassengerController : Controller
    {

        private SystemDbContext _context;

        public PassengerController(SystemDbContext context)
        {
            this._context= context;
        }
        
		public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp() 
        {
            return View();
        }
		
		[HttpPost]
        public IActionResult SignUp(Passenger passenger)
        {
            bool Empty = CheckEmpty(passenger);
            bool duplicate = CheckDuplicate(passenger.UserName,passenger.EmailAddress,passenger.PhoneNumber);

            if (Empty)
            {
                if (duplicate)
                {
                    _context.passenger.Add(passenger);
                    _context.SaveChanges();

                    TempData["Msg"] = "Congrats You Signed Up";
                    return View();
                }
                else
                {
                    TempData["Msg"] = "The data is Used";
                    return View();
                }
            }
            else
            {
                TempData["Msg"] = "please fill Data";
				return View();
            }
        }
		  
		public bool CheckDuplicate(String username, String emailAddress, int phoneNumber)
        {
            Passenger user = _context.passenger.Where(u=>u.UserName.Equals(username)&&
            u.EmailAddress.Equals(emailAddress) &&
            u.PhoneNumber.Equals(phoneNumber)).FirstOrDefault();

            if(user!=null)
            {
                return false;
            }
            else
            {
                return true;
            }
		}
		public bool CheckEmpty(Passenger passenger)
        {
            if (String.IsNullOrEmpty(passenger.Name)) return false;
            else if (String.IsNullOrEmpty(passenger.UserName)) return false;
            else if (String.IsNullOrEmpty(passenger.Password)) return false;
            else if (String.IsNullOrEmpty(passenger.EmailAddress)) return false;
            else if (String.IsNullOrEmpty(passenger.Gender)) return false;
            else return true;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login login)
        {
			if (ModelState.IsValid)
            {
				String username = login.Username;
				String password = login.Password;

				Passenger passenger = _context.passenger.Where(u => u.UserName.Equals(username) &&
				u.Password.Equals(password)).FirstOrDefault();

                Admin admin = _context.admin.Where(a=> a.UserName.Equals(username) &&
				a.Password.Equals(password)).FirstOrDefault();

				if (passenger!=null)
                {
                    HttpContext.Session.SetInt32("PassengerID",passenger.PassengerID );
                    return RedirectToAction("BusTripList");
                }
                else if(admin!=null)
                {
                    HttpContext.Session.SetInt32("adminID",admin.AdminID);
                    return RedirectToAction("Index", "BusTrip");
                }
                else
                {
                    TempData["Msg"] = "Passenger not found";
                }
			}
            else
            {

            }
            return View();
        }

        public IActionResult BusTripList()
        {
            int passengerId = (int)HttpContext.Session.GetInt32("PassengerID");

            List<int> lst=_context.passenger_bustrip.Where(
                t => t.passenger.PassengerID == passengerId
                ).Select(s=>s.bustrip.BusTripID).ToList();

            List<BusTrip> lst_BusTrip=_context.busTrip.Where(
                t => lst.Contains(t.BusTripID)==false
                ).ToList();

            return View(lst_BusTrip);
        }

        public IActionResult SelectTrip(int id)
        {
            int BusTripID = id;
            int passengerId=(int)HttpContext.Session.GetInt32("PassengerID");

            Passenger_BusTrip passenger_bustrip=new Passenger_BusTrip();

            passenger_bustrip.passenger = _context.passenger.Find(passengerId);
            passenger_bustrip.bustrip = _context.busTrip.Find(BusTripID);

            _context.passenger_bustrip.Add(passenger_bustrip);
            _context.SaveChanges();

            return RedirectToAction("ListTrips");
        }


        public IActionResult ListTrips()
        {
            int passengerId = (int)HttpContext.Session.GetInt32("PassengerID");

            List<int> lst_BusTrip=_context.passenger_bustrip.Where(
                t => t.passenger.PassengerID == passengerId
                ).Select(s=>s.bustrip.BusTripID).ToList();

            List<BusTrip> lst = _context.busTrip.Where(
                t => lst_BusTrip.Contains(t.BusTripID)
                ).ToList();

            return View(lst);
        }
        
        public IActionResult CancleTrip(int BusTripID)
        {
            int passengerId = (int)HttpContext.Session.GetInt32("PassengerID");

            Passenger_BusTrip passenger_BusTrip=_context.passenger_bustrip.Where(
                t => t.passenger.PassengerID == passengerId && t.bustrip.BusTripID==BusTripID
                ).FirstOrDefault();

            _context.passenger_bustrip.Remove(passenger_BusTrip); 
            _context.SaveChanges();

            return RedirectToAction("ListTrips");
        }
    }
}