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
    public class BuoiChieuController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/BuoiChieu
        public ActionResult Index()
        {
            var buoiChieux = db.BuoiChieux.Include(b => b.Phim).Include(b => b.PhongChieu).Include(b => b.RapPhim);
            return View(buoiChieux.ToList());
        }

        // GET: Admin/BuoiChieu/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Entities.BuoiChieu buoiChieu = db.BuoiChieux.Find(id);
            if (buoiChieu == null)
            {
                return HttpNotFound();
            }
            var i = db.Ves.Where(x => x.MaBuoiChieu == buoiChieu.MaBuoiChieu).Count();
            buoiChieu.SoVeDaBan = i;
            double tien = (double)(buoiChieu.GiaVe * i);          
            buoiChieu.TongTien = tien;
            return View(buoiChieu);
        }

        // GET: Admin/BuoiChieu/Create
        public ActionResult Create(string ma)
        {
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim");
            /*ViewBag.MaPhong = new SelectList(db.PhongChieux, "MaPhong", "MaRap");*/
            ViewBag.MaRap = new SelectList(db.RapPhims, "MaRap", "TenRap");
            List<Models.Entities.PhongChieu> listPhong = db.PhongChieux.Where(c => c.MaRap == "RP01").ToList();
            ViewBag.MaPhong = new SelectList(listPhong, "MaPhong", "TenPhong"); 
            return View();
        }
        public string layMaRap(string Ma)
        {
            string Id="";

            return Id;
        }
        // POST: Admin/BuoiChieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBuoiChieu,MaPhim,MaRap,MaPhong,NgayChieu,GioChieu,GiaVe,SoVeDaBan,TongTien")] Models.Entities.BuoiChieu buoiChieu)
        {
            if (ModelState.IsValid)
            {
                string macuoi = db.BuoiChieux.ToList().Last().MaBuoiChieu;
                string tmp = macuoi.Replace("BC", "");
                int a = 0;
                if (tmp[0] == '0') {
                    tmp = tmp.Replace("0", "");
                }
                a = int.Parse(tmp) + 1;   
                if (a < 10)
                    buoiChieu.MaBuoiChieu = "BC0" + a.ToString();
                else
                    buoiChieu.MaBuoiChieu = "BC" + a.ToString();
                db.BuoiChieux.Add(buoiChieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", buoiChieu.MaPhim);
            ViewBag.MaPhong = new SelectList(db.PhongChieux, "MaPhong", "MaRap", buoiChieu.MaPhong);
            ViewBag.MaRap = new SelectList(db.RapPhims, "MaRap", "TenRap", buoiChieu.MaRap);
            return View(buoiChieu);
        }

        // GET: Admin/BuoiChieu/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Entities.BuoiChieu buoiChieu = db.BuoiChieux.Find(id);
            if (buoiChieu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", buoiChieu.MaPhim);
            List<Models.Entities.PhongChieu> listPhong = db.PhongChieux.Where(c => c.MaRap == buoiChieu.MaRap).ToList();
            ViewBag.MaPhong = new SelectList(listPhong, "MaPhong", "TenPhong");
            /*ViewBag.MaPhong = new SelectList(db.PhongChieux, "MaPhong", "TenPhong", buoiChieu.MaPhong);*/
            ViewBag.MaRap = new SelectList(db.RapPhims, "MaRap", "TenRap", buoiChieu.MaRap);
            return View(buoiChieu);
        }

        // POST: Admin/BuoiChieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBuoiChieu,MaPhim,MaRap,MaPhong,NgayChieu,GioChieu,GiaVe,SoVeDaBan,TongTien")] Models.Entities.BuoiChieu buoiChieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buoiChieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", buoiChieu.MaPhim);
            List<Models.Entities.PhongChieu> listPhong = db.PhongChieux.Where(c => c.MaRap == buoiChieu.MaRap).ToList();
            ViewBag.MaPhong = new SelectList(listPhong, "MaPhong", "TenPhong");
            /*ViewBag.MaPhong = new SelectList(db.PhongChieux, "MaPhong", "TenPhong", buoiChieu.MaPhong);*/
            ViewBag.MaRap = new SelectList(db.RapPhims, "MaRap", "TenRap", buoiChieu.MaRap);
            return View(buoiChieu);
        }

        // GET: Admin/BuoiChieu/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Entities.BuoiChieu buoiChieu = db.BuoiChieux.Find(id);
            if (buoiChieu == null)
            {
                return HttpNotFound();
            }
            return View(buoiChieu);
        }

        // POST: Admin/BuoiChieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {          
            Models.Entities.BuoiChieu buoiChieu = db.BuoiChieux.Find(id);
            db.BuoiChieux.Remove(buoiChieu);
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

        public ActionResult ChonPhong(string ma)
        {
            /*List<PhongChieu> listPhong = db.PhongChieux.Where(c => c.MaRap == ma).ToList();
            *//*ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim");
            *//*ViewBag.MaPhong = new SelectList(db.PhongChieux, "MaPhong", "MaRap");*//*
            ViewBag.MaRap = new SelectList(db.RapPhims, "MaRap", "TenRap");*//*
            ViewBag.MaPhong = new SelectList(listPhong, "MaPhong", "TenPhong"); 
            return PartialView("Phong", listPhong);*/
            if (ma == null)
                ma = "RP01";
            List<Models.Entities.PhongChieu> listPhong = db.PhongChieux.Where(c => c.MaRap == ma).ToList();
            ViewBag.MaPhong = new SelectList(listPhong, "MaPhong", "TenPhong");
            return PartialView("Phong", listPhong);
        }


    }
}
