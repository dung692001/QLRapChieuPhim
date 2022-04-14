using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiTapLon_Web.Models.Entities;

namespace BaiTapLon_Web.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private MyDBContext db = new MyDBContext();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var phims = db.Phims.Include(p => p.HangSX).Include(p => p.NuocSanXuat).Include(p => p.Theloai);
            return View(phims.ToList());
        }

    }
}