using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EYM.Entities;

namespace EYM.Presentation.Admin.Controllers
{
    public class ProductTemplatesController : Controller
    {
        private DinnerContext db = new DinnerContext();

        // GET: ProductTemplates
        public ActionResult Index()
        {
            var productTemplates = db.ProductTemplates.Include(p => p.ProductType).Include(p => p.Provider);
            return View(productTemplates.ToList());
        }

        // GET: ProductTemplates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTemplate productTemplate = db.ProductTemplates.Find(id);
            if (productTemplate == null)
            {
                return HttpNotFound();
            }
            return View(productTemplate);
        }

        // GET: ProductTemplates/Create
        public ActionResult Create()
        {
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "Name");
            ViewBag.ProviderId = new SelectList(db.Providers, "ProviderId", "Name");
            return View();
        }

        // POST: ProductTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductTemplateId,Name,IsActive,Description,ImagePath,ProviderId,ProductTypeId")] ProductTemplate productTemplate)
        {
            if (ModelState.IsValid)
            {
                db.ProductTemplates.Add(productTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "Name", productTemplate.ProductTypeId);
            ViewBag.ProviderId = new SelectList(db.Providers, "ProviderId", "Name", productTemplate.ProviderId);
            return View(productTemplate);
        }

        // GET: ProductTemplates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTemplate productTemplate = db.ProductTemplates.Find(id);
            if (productTemplate == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "Name", productTemplate.ProductTypeId);
            ViewBag.ProviderId = new SelectList(db.Providers, "ProviderId", "Name", productTemplate.ProviderId);
            return View(productTemplate);
        }

        // POST: ProductTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductTemplateId,Name,IsActive,Description,ImagePath,ProviderId,ProductTypeId")] ProductTemplate productTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "Name", productTemplate.ProductTypeId);
            ViewBag.ProviderId = new SelectList(db.Providers, "ProviderId", "Name", productTemplate.ProviderId);
            return View(productTemplate);
        }

        // GET: ProductTemplates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTemplate productTemplate = db.ProductTemplates.Find(id);
            if (productTemplate == null)
            {
                return HttpNotFound();
            }
            return View(productTemplate);
        }

        // POST: ProductTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductTemplate productTemplate = db.ProductTemplates.Find(id);
            db.ProductTemplates.Remove(productTemplate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
