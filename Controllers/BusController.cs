using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTRS.Controllers
{
    public class BusController : Controller
    {
        private SystemDbContext _context;
        public BusController(SystemDbContext context)
        {
            this._context = context;
        }
        // GET: BusController
        public ActionResult Index()
        {
            return View(_context.bus.ToList());
        }

        // GET: BusController/Details/5
        public ActionResult Details(int id)
        {
			Bus bus = _context.bus.Find(id);
            return View(bus);
		}

        // GET: BusController/Create
        public ActionResult Create()
        {
            ViewBag.BusTrip = _context.busTrip.ToList();
            return View();
        }

        // POST: BusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            int adminid = (int)HttpContext.Session.GetInt32("adminID");

            Admin admin = _context.admin.Where(
                  a => a.AdminID == adminid
                  ).FirstOrDefault();

            string CaptinName = form["CaptinName"];
            int NumberOfSeats = int.Parse(form["NumberOfSeats"]);
            int BusTripID = int.Parse(form["BusTripID"]);

            Bus bus = new Bus();
            bus.CaptinName = CaptinName;
            bus.NumberOfSeats = NumberOfSeats;
            bus.bustrip = _context.busTrip.Find(BusTripID);

            bus.admin = admin;

            _context.bus.Add(bus);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: BusController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.BusTrip = _context.busTrip.ToList();
			Bus bus = _context.bus.Find(id);
			return View(bus);
        }

        // POST: BusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection form)
        {
            int adminid = (int)HttpContext.Session.GetInt32("adminID");

            Admin admin = _context.admin.Where(
                  a => a.AdminID == adminid
                  ).FirstOrDefault();

            string CaptinName = form["CaptinName"];
            int NumberOfSeats = int.Parse(form["NumberOfSeats"]);
            int BusTripID = int.Parse(form["BusTripID"]);

            Bus bus = _context.bus.Find(id);
            bus.CaptinName = CaptinName;
            bus.NumberOfSeats = NumberOfSeats;
            bus.bustrip = _context.busTrip.Find(BusTripID);

            bus.admin = admin;

            _context.bus.Update(bus);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: BusController/Delete/5
        public ActionResult Delete(int id)
        {
            Bus bus = _context.bus.Find(id);
            return View(bus);
        }

        // POST: BusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Bus bus)
        {
            try
            {
                _context.bus.Remove(bus);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
