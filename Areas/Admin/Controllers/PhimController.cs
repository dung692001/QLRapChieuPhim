using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaiTapLon_Web.Models.Entities;
using System.IO;
using System.Web.Services.Description;

namespace BaiTapLon_Web.Areas.Admin.Controllers
{
    public class PhimController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/Phim
        public ActionResult Index()
        {
            var phims = db.Phims.Include(p => p.HangSX).Include(p => p.NuocSanXuat).Include(p => p.Theloai);
            return View(phims.ToList());
        }

        // GET: Admin/Phim/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // GET: Admin/Phim/Create
        public ActionResult Create()
        {
            ViewBag.MaHang = new SelectList(db.HangSXes, "MaHangSX", "TenHangSX");
            ViewBag.MaNuocSX = new SelectList(db.NuocSanXuats, "NuocSX", "TenNuocSX");
            ViewBag.MaTheLoai = new SelectList(db.Theloais, "MaTheLoai", "TenTheLoai");
            return View();
        }

        // POST: Admin/Phim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhim,TenPhim,MaNuocSX,MaHang,MaTheLoai,DaoDien,NuDienVienChinh,NamDienVienChinh,NoiDungChinh,NgayKhoiChieu,NgayKetThuc,TongChiPhi,Anh,ThoiLuong,NamSX,Trailer")] Phim phim, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                db.Phims.Add(phim);
                db.SaveChanges();
                if(uploadhinh != null && uploadhinh.ContentLength > 0) {
                    string id = db.Phims.ToList().Last().MaPhim;
                    string _FileName = "";
                    _FileName = uploadhinh.FileName;
                    string _path = Path.Combine(Server.MapPath("~/Public/images/AnhPhim"), _FileName);
                    uploadhinh.SaveAs(_path);

                    Phim uphim = db.Phims.FirstOrDefault(x => x.MaPhim == id);
                    uphim.Anh = _File;
                    db.SaveChanges();
                    
                }
                return RedirectToAction("Index");
            }

            ViewBag.MaHang = new SelectList(db.HangSXes, "MaHangSX", "TenHangSX", phim.MaHang);
            ViewBag.MaNuocSX = new SelectList(db.NuocSanXuats, "NuocSX", "TenNuocSX", phim.MaNuocSX);
            ViewBag.MaTheLoai = new SelectList(db.Theloais, "MaTheLoai", "TenTheLoai", phim.MaTheLoai);
            return View(phim);
        }*/
        [HttpPost]
        public ActionResult Create(Phim phim, HttpPostedFileBase Anh)
        {

            if (ModelState.IsValid) 
            {
                string macuoi = db.Phims.ToList().Last().MaPhim;
                string tmp = macuoi.Replace("Film", "");
                int a = 0;
                if (tmp[0] == '0') {
                    tmp = tmp.Replace("0", "");
                }
                a = int.Parse(tmp) + 1;
                if(a<10)
                    phim.MaPhim = "Film0" + a.ToString();
                else
                phim.MaPhim = "Film" + a.ToString();     
                db.Phims.Add(phim);
                db.SaveChanges();           
                if (Anh != null && Anh.ContentLength > 0) {
                        string id = phim.MaPhim;
                        string _FileName = "";
                        _FileName = Anh.FileName;
                        string _path = Path.Combine(Server.MapPath("~/Public/images/AnhPhim"), _FileName);
                        Anh.SaveAs(_path);

                        Phim uphim = db.Phims.FirstOrDefault(x => x.MaPhim == id);
                        uphim.Anh = _FileName;
                        db.SaveChanges();
                }
            return RedirectToAction("Index");
            }
            ViewBag.MaHang = new SelectList(db.HangSXes, "MaHangSX", "TenHangSX", phim.MaHang);
            ViewBag.MaNuocSX = new SelectList(db.NuocSanXuats, "NuocSX", "TenNuocSX", phim.MaNuocSX);
            ViewBag.MaTheLoai = new SelectList(db.Theloais, "MaTheLoai", "TenTheLoai", phim.MaTheLoai);
            return View(phim);
        }
        

        // GET: Admin/Phim/Edit/5
        public ActionResult Edit(string id)
        {        
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHang = new SelectList(db.HangSXes, "MaHangSX", "TenHangSX", phim.MaHang);
            ViewBag.MaNuocSX = new SelectList(db.NuocSanXuats, "NuocSX", "TenNuocSX", phim.MaNuocSX);
            ViewBag.MaTheLoai = new SelectList(db.Theloais, "MaTheLoai", "TenTheLoai", phim.MaTheLoai);
            return View(phim);
        }

        // POST: Admin/Phim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Phim phim, HttpPostedFileBase Anh)
        {
            if (ModelState.IsValid) {
                db.Entry(phim).State = EntityState.Modified;
                db.SaveChanges();
                if (Anh != null && Anh.ContentLength > 0) {
                    string id = phim.MaPhim;
                    string _FileName = "";
                    _FileName = Anh.FileName;
                    string _path = Path.Combine(Server.MapPath("~/Public/images/AnhPhim"), _FileName);
                    Anh.SaveAs(_path);
                    Phim uphim = db.Phims.FirstOrDefault(x => x.MaPhim == id); 
                    uphim.Anh = _FileName;
                    db.SaveChanges();
                }
            
            return RedirectToAction("Index");
            }
            ViewBag.MaHang = new SelectList(db.HangSXes, "MaHangSX", "TenHangSX", phim.MaHang);
            ViewBag.MaNuocSX = new SelectList(db.NuocSanXuats, "NuocSX", "TenNuocSX", phim.MaNuocSX);
            ViewBag.MaTheLoai = new SelectList(db.Theloais, "MaTheLoai", "TenTheLoai", phim.MaTheLoai);
            return View(phim);
        }

        // GET: Admin/Phim/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            var p = db.BuoiChieux.Where(d => d.MaPhim == id);
            if (p.Count() >= 1)
            {
                return RedirectToAction("DenyDelete");
            }
            return View(phim);
        }

        // POST: Admin/Phim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Phim phim = db.Phims.Find(id);
            db.Phims.Remove(phim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DenyDelete()
        {
            return View(); 
        }
    }
}
