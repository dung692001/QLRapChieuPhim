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
    public class NuocSanXuatsController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/NuocSanXuats
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult dsNuocSX()
        {

            var dsNuocSX = (from da in db.NuocSanXuats
                            select new {
                                NuocSX = da.NuocSX,
                                TenNuocSX = da.TenNuocSX

                            }).ToList();

            //return View("Index", dsNuocSX);
            return Json(new { dsNuocSX = dsNuocSX }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult addNuocSanXuat(string manuocsx, string tennuocsx)
        {
            try {
                var l = new NuocSanXuat();
                l.NuocSX = manuocsx;
                l.TenNuocSX = tennuocsx;
                db.NuocSanXuats.Add(l);
                db.SaveChanges();

                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }




        }
        [HttpGet]
        public ActionResult chiTietNuocSX(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.NuocSanXuats.SingleOrDefault(x => x.NuocSX == id);

            return Json(new { code = 200, da = da }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult editNuocSanXuat(string manuocsx, string tennuocsx)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.NuocSanXuats.SingleOrDefault(x => x.NuocSX == manuocsx);

            da.TenNuocSX = tennuocsx;

            db.SaveChanges();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            //return View("Index");
        }
        [HttpPost]
        public ActionResult xoaNuocSanXuat(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var p = db.Phims.Where(d => d.MaNuocSX == id);

            if (p.Count() >= 1) return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            else {
                NuocSanXuat da = db.NuocSanXuats.Find(id);
                db.NuocSanXuats.Remove(da);
                db.SaveChanges();
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }


            //  return View("Index");

        }
    }
}
