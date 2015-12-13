using System;
using System.Data;
using System.Web.Mvc;
using EYM.DBServices.Interfaces;
using EYM.Entities;
using EYM.Logging.Interfaces;

namespace EYM.Presentation.Admin.Controllers
{
	public class ProvidersController : Controller
	{
		private IDBService<Provider> _db { get; set; }
		private ILogging _log { get; set; }

		public ProvidersController(IDBService<Provider> db, ILogging logging)
		{
			_db = db;
			_log = logging;
		}

		// GET: Providers
		public ActionResult Index()
		{
			return View(_db.GetAll());
		}

		// GET: Providers/Details/5
		public ActionResult Details(int id)
		{
			Provider provider = _db.Get(id);
			return View(provider);
		}

		// GET: Providers/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Providers/Create
		[HttpPost]
		public ActionResult Create(Provider provider)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_db.Add(provider);
					_log.Info("A new provider record has been created");
					return RedirectToAction("Index");

				}
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Error during creating a provider record");
			}
			return View(provider);
		}

		// GET: Providers/Edit/5
		public ActionResult Edit(int id)
		{
			Provider provider = _db.Get(id);
			return View(provider);
		}

		// POST: Providers/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, Provider provider)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_db.Update(provider);
					_log.Info(String.Format("Provider record with Id={0} has been updated", id));
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				_log.Error(ex, String.Format("Error during updating a provider record with Id={0}", id));

			}
			return View(provider);
		}

		// GET: Providers/Delete/5
		public ActionResult Delete(bool? saveChangesError = false, int id = 0)
		{
			if (saveChangesError.GetValueOrDefault())
			{
				ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
			}
			Provider provider = _db.Get(id);
			return View(provider);
		}

		// POST: Providers/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				Provider provider = _db.Get(id);
				_db.Delete(provider);
				
			}
			catch (DataException ex)
			{
				_log.Error(ex, String.Format("Error during delete of a provider record with Id={0}", id));
				return RedirectToAction("Delete", new { id = id, saveChangesError = true });
			}
			return RedirectToAction("Index");
		}
	}
}
