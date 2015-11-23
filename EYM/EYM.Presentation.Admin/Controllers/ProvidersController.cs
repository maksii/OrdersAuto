using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EYM.DBServices.Interfaces;
using EYM.Entities;

namespace EYM.Presentation.Admin.Controllers
{
    public class ProvidersController : Controller
    {
	    public IDBService<Provider> _db { get; set; }

		//public ProvidersController()
		//{
			
		//}

		public ProvidersController(IDBService<Provider> db)
		{
			_db = db;
		}

        // GET: Providers
        public ActionResult Index()
        {
	        
			//var service = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IDBService<Provider>));
			//((IDBService<Provider>)service).Serve();
			return View(_db.Serve());
        }

        // GET: Providers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Providers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Providers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Providers/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Providers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Providers/Delete/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
