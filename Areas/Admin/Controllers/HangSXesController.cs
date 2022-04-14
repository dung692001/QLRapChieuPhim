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
    public class HangSXesController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/HangSXs
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult dsHangSX()
        {

            var dsHangSX = (from da in db.HangSXes
                            select new {
                                MaHangSX = da.MaHangSX,
                                TenHangSX = da.TenHangSX

                            }).ToList();

            //return View("Index", dsHangSX);
            return Json(new { dsHangSX = dsHangSX }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult addHangSX(string mahangsx, string tenhangsx)
        {
            try {
                var l = new HangSX();
                l.MaHangSX = mahangsx;
                l.TenHangSX = tenhangsx;
                db.HangSXes.Add(l);
                db.SaveChanges();

                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public ActionResult chiTietHangSX(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.HangSXes.SingleOrDefault(x => x.MaHangSX == id);

            return Json(new { code = 200, da = da }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult editHangSX(string mahangsx, string tenhangsx)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.HangSXes.SingleOrDefault(x => x.MaHangSX == mahangsx);

            da.TenHangSX = tenhangsx;

            db.SaveChanges();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            //return View("Index");
        }
        [HttpPost]
        public ActionResult xoaHangSX(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var p = db.Phims.Where(d => d.MaHang == id);

            if (p.Count() >= 1) return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);

            else {
                HangSX da = db.HangSXes.Find(id);
                db.HangSXes.Remove(da);
                db.SaveChanges();
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            //  return View("Index");

        }
    }
}
