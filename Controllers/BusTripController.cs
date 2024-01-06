using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTRS.Controllers
{
	public class BusTripController : Controller
	{
		private SystemDbContext _context;

		public BusTripController(SystemDbContext context)
		{
			this._context = context;
		}

		// GET: BusTrip
		public ActionResult Index()
		{

			return View(_context.busTrip.ToList());
		}

		// GET: BusTrip/Details/5
		public ActionResult Details(int id)
		{
			BusTrip bustrip=_context.busTrip.Find(id);

			return View(bustrip);
		}

		// GET: BusTrip/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: BusTrip/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(BusTrip bustrip)
		{
			int adminID =(int)HttpContext.Session.GetInt32("adminID");

			Admin admin = _context.admin.Where(a => a.AdminID==adminID).FirstOrDefault();

			bustrip.admin = admin;

			_context.busTrip.Add(bustrip);
			_context.SaveChanges();
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: BusTrip/Edit/5
		public ActionResult Edit(int id)
		{
			BusTrip bustrip = _context.busTrip.Find(id);

			return View(bustrip);
		}

		// POST: BusTrip/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection form)
		{
			int adminid = (int)HttpContext.Session.GetInt32("adminID");

			Admin admin = _context.admin.Where(
				  a => a.AdminID == adminid
				  ).FirstOrDefault();

			string TripDistination = form["TripDistination"];
			DateTime StartDate = DateTime.Parse(form["StartDate"]);
			DateTime EndDate = DateTime.Parse(form["EndDate"]);
			int BusNumber = int.Parse(form["BusNumber"]);

			BusTrip bustrip=_context.busTrip.Find(id);
			bustrip.TripDistination = TripDistination; 
			bustrip.BusNumber = BusNumber;
			bustrip.StartDate = StartDate;
			bustrip.EndDate = EndDate;

			bustrip.admin = admin;

			_context.busTrip.Update(bustrip);
			_context.SaveChanges();

			return RedirectToAction("Index");


			
		}

		// GET: BusTrip/Delete/5
		public ActionResult Delete(int id)
		{
			BusTrip bustrip = _context.busTrip.Find(id);
			return View(bustrip);
		}

		// POST: BusTrip/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, BusTrip bustrip)
		{
			try
			{
				_context.busTrip.Remove(bustrip); 
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
