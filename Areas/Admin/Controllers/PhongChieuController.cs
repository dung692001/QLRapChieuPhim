using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaiTapLon_Web.Models.Entities;

namespace BaiTapLon_Web.Areas.Admin.Controllers
{
    public class PhongChieuController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/PhongChieu
        public ActionResult Index()
        {
            return View(db.PhongChieux.ToList());
        }

        // GET: Admin/PhongChieu/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Entities.PhongChieu phongChieu = db.PhongChieux.Find(id);
            if (phongChieu == null)
            {
                return HttpNotFound();
            }
            return View(phongChieu);
        }

        // GET: Admin/PhongChieu/Create
        public ActionResult Create()
        {
            ViewBag.MaRap = new SelectList(db.RapPhims, "MaRap", "TenRap");
            return View();
        }

        // POST: Admin/PhongChieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhong,MaRap,TenPhong,TongGhe")] Models.Entities.PhongChieu phongChieu)
        {
            if (ModelState.IsValid)
            {
                db.PhongChieux.Add(phongChieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phongChieu);
        }

        // GET: Admin/PhongChieu/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.MaRap = new SelectList(db.RapPhims, "MaRap", "TenRap");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Entities.PhongChieu phongChieu = db.PhongChieux.Find(id);
            if (phongChieu == null)
            {
                return HttpNotFound();
            }
            return View(phongChieu);
        }

        // POST: Admin/PhongChieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhong,MaRap,TenPhong,TongGhe")] Models.Entities.PhongChieu phongChieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phongChieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phongChieu);
        }

        // GET: Admin/PhongChieu/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Entities.PhongChieu phongChieu = db.PhongChieux.Find(id);
            if (phongChieu == null)
            {
                return HttpNotFound();
            }
            var p = db.BuoiChieux.Where(d => d.MaPhong == id);
            if (p.Count() >= 1) {
                return RedirectToAction("DenyDelete");
            }
            return View(phongChieu);
        }

        // POST: Admin/PhongChieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Models.Entities.PhongChieu phongChieu = db.PhongChieux.Find(id);
            db.PhongChieux.Remove(phongChieu);
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

        public ActionResult DenyDelete()
        {
            return View();
        }
    }
}
