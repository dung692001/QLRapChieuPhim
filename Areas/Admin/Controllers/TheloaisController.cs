using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaiTapLon_Web.Models.Entities;
using Newtonsoft.Json;

namespace BaiTapLon_Web.Areas.Admin.Controllers
{
    public class TheloaisController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/Theloais
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult dsTheLoai()
        {
            var dsTL = (from da in db.Theloais
                        select new {
                            MaTheLoai = da.MaTheLoai,
                            TenTheLoai = da.TenTheLoai

                        }).ToList();
            return Json(new { dsTL = dsTL }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult addTheLoai(string matl, string tentl)
        {
            try {
                var l = new Theloai();
                l.MaTheLoai = matl;
                l.TenTheLoai = tentl;
                db.Theloais.Add(l);
                db.SaveChanges();

                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch {
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }
            //return RedirectToAction("Index");



        }
        public ActionResult Add()
        {
            return PartialView("modal_Add");
        }
        [HttpPost]

        public ActionResult Add(string matl, string tentl)
        {

            try {
                var l = new Theloai();
                l.MaTheLoai = matl;
                l.TenTheLoai = tentl;
                db.Theloais.Add(l);
                db.SaveChanges();

                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch {
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }


        }
        //public ActionResult Add(string matl, string tentl)
        //{

        //    try
        //    {
        //        var l = new Theloai();
        //        l.MaTheLoai = matl;
        //        l.TenTheLoai = tentl;
        //        db.Theloais.Add(l);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");

        //    }
        //    catch
        //    {
        //        var l = new Theloai();
        //        l.MaTheLoai = matl;
        //        l.TenTheLoai = tentl;
        //        db.Theloais.Add(l);
        //        db.SaveChanges();
        //        return RedirectToAction("Add");
        //        return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        var l = new Theloai();
        //        l.MaTheLoai = matl;
        //        l.TenTheLoai = tentl;
        //        db.Theloais.Add(l);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    else return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);

        //}
        [HttpGet]
        public ActionResult chiTietTL(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.Theloais.SingleOrDefault(x => x.MaTheLoai == id);

            return Json(new { code = 200, da = da }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]

        public ActionResult editTheLoai(string matl, string tentl)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.Theloais.SingleOrDefault(x => x.MaTheLoai == matl);

            da.TenTheLoai = tentl;

            db.SaveChanges();

            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            //return View("Index");
        }
        [HttpPost]
        public ActionResult xoaTheLoai(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var p = db.Phims.Where(d => d.MaTheLoai == id);

            if (p.Count() >= 1) return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            else {
                Theloai da = db.Theloais.Find(id);
                db.Theloais.Remove(da);
                db.SaveChanges();
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }


            //  return View("Index");

        }
    }
}
