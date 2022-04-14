using BaiTapLon_Web.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapLon_Web.Controllers
{
    public class MovieWebController : Controller
    {
        // GET: MovieWeb
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RenderProduct()
        {
            MyDBContext db = new MyDBContext();
            List<Phim> listPhim = db.Phims.ToList();
            return PartialView("MovieTheaterWeb_Main", listPhim);
        }

    }
}