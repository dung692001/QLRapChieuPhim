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
    public class RapPhimController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/RapPhim
        public ActionResult Index()
        {
            return View(db.RapPhims.ToList());
        }

        // GET: Admin/RapPhim/Details/5
        public ActionResult Details(string id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RapPhim rapPhim = db.RapPhims.Find(id);
            if (rapPhim == null) {
                return HttpNotFound();
            }
            return View(rapPhim);
        }

        // GET: Admin/RapPhim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RapPhim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaRap,TenRap,DiaChi,DienThoai,SoPhong,TongSoGhe")] RapPhim rapPhim)
        {
            if (ModelState.IsValid) {
                db.RapPhims.Add(rapPhim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rapPhim);
        }

        // GET: Admin/RapPhim/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RapPhim rapPhim = db.RapPhims.Find(id);
            if (rapPhim == null) {
                return HttpNotFound();
            }
            return View(rapPhim);
        }

        // POST: Admin/RapPhim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaRap,TenRap,DiaChi,DienThoai,SoPhong,TongSoGhe")] RapPhim rapPhim)
        {
            if (ModelState.IsValid) {
                db.Entry(rapPhim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rapPhim);
        }

        // GET: Admin/RapPhim/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RapPhim rapPhim = db.RapPhims.Find(id);
            if (rapPhim == null) {
                return HttpNotFound();
            }
            return View(rapPhim);
        }

        // POST: Admin/RapPhim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RapPhim rapPhim = db.RapPhims.Find(id);
            db.RapPhims.Remove(rapPhim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
