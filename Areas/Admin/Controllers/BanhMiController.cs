using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanBanhMi.Models;

namespace WebBanBanhMi.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BanhMiController : Controller
    {
        private BanhMiModelContext db = new BanhMiModelContext();

        // GET: Admin/BanhMi
        public ActionResult Index()
        {
            var banhMis = db.BanhMis.Include(b => b.Category);
            return View(banhMis.ToList());
        }

        // GET: Admin/BanhMi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BanhMi banhMi = db.BanhMis.Find(id);
            if (banhMi == null)
            {
                return HttpNotFound();
            }
            return View(banhMi);
        }

        // GET: Admin/BanhMi/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/BanhMi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Description,Image,CategoryId")] BanhMi banhMi)
        {
            if (ModelState.IsValid)
            {
                db.BanhMis.Add(banhMi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", banhMi.CategoryId);
            return View(banhMi);
        }

        // GET: Admin/BanhMi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BanhMi banhMi = db.BanhMis.Find(id);
            if (banhMi == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", banhMi.CategoryId);
            return View(banhMi);
        }

        // POST: Admin/BanhMi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Description,Image,CategoryId")] BanhMi banhMi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banhMi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", banhMi.CategoryId);
            return View(banhMi);
        }

        // GET: Admin/BanhMi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BanhMi banhMi = db.BanhMis.Find(id);
            if (banhMi == null)
            {
                return HttpNotFound();
            }
            return View(banhMi);
        }

        // POST: Admin/BanhMi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BanhMi banhMi = db.BanhMis.Find(id);
            db.BanhMis.Remove(banhMi);
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
