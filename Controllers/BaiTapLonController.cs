using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiTapLon_Web.Models.Entities;
namespace BaiTapLon_Web.Controllers
{
    public class BaiTapLonController : Controller
    {
        private MyDBContext db = new MyDBContext();
        // GET: BaiTapLon
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RenderPhim()
        {
            //List<Phim> lstPhim = db.Phims.ToList();
            IQueryable<Phim> lstPhim = db.Phims.Take(8);
            return PartialView("Main", lstPhim);
        }

        public ActionResult ChiTietPhim(string maPhim)
        {
            /*var phim = db.Phims.Where(h => h.MaPhim == maPhim);*/
            var phim = db.Phims.FirstOrDefault(h => h.MaPhim == maPhim);
            return PartialView("ChiTietPhim", phim);
        }

        [Authorize]
        public ActionResult MuaVe()
        {

            //return PartialView("Main");
            return View();
        }
    }
}