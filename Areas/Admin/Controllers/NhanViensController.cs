using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using BaiTapLon_Web.Models.Entities;

namespace BaiTapLon_Web.Areas.Admin.Controllers
{
    public class NhanViensController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/NhanViens
        public ActionResult Index()
        {
            ViewBag.MaRap = new SelectList(db.RapPhims, "MaRap", "TenRap");

            return View();
        }
        public ActionResult Create()
        {
            ViewBag.MaRap = new SelectList(db.RapPhims, "MaRap", "TenRap");
            //return PartialView("modal_Th");
            return View();
        }
        [HttpPost]
        public ActionResult Create(NhanVien nv, HttpPostedFileBase uploadhinh)
        {
            ViewBag.MaRap = new SelectList(db.RapPhims, "MaRap", "TenRap");
            if (ModelState.IsValid) {
                string id_last = db.NhanViens.ToList().Last().MaNhanVien;
                string tmp = id_last.Replace("NV", "");
                int a = 0;
                if (tmp[0] == '0') {
                    tmp = tmp.Replace("0", "");
                }
                a = int.Parse(tmp) + 1;
                if (a < 10) {
                    nv.MaNhanVien = "NV0" + a.ToString();
                }
                else {
                    nv.MaNhanVien = "NV" + a.ToString();
                }
                string mahoa = Encode(nv.MatKhau);
                nv.MatKhau = mahoa;
                db.NhanViens.Add(nv);
                db.SaveChanges();
                if (uploadhinh != null && uploadhinh.ContentLength > 0) {

                    string id = nv.MaNhanVien;
                    string _FileName = "";
                    _FileName = uploadhinh.FileName;
                    string _path = Path.Combine(Server.MapPath("~/Public/images/NhanVien"), _FileName);
                    uploadhinh.SaveAs(_path);
                    NhanVien unv = db.NhanViens.FirstOrDefault(x => x.MaNhanVien == id);
                    unv.Anh = _FileName;
                    // db.NhanViens.Add(unv);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            else
                //return Json(new { code =500 }, JsonRequestBehavior.AllowGet);
                return View(nv);
            // return RedirectToAction("Loi");
        }
        [HttpPost]
        public ActionResult Loi(string id)
        {
            var p = db.Phims.Where(d => d.MaTheLoai == id);

            if (p.Count() >= 1) return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);

            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            ViewBag.MaRap = new SelectList(db.RapPhims, "MaRap", "TenRap");
            if (id == null || ViewBag.Marap == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null) {
                return HttpNotFound();
            }
            return View(nhanVien);
        }
        [HttpPost]
        public ActionResult Edit(NhanVien nv, HttpPostedFileBase uploadhinh)
        {
            NhanVien unv = db.NhanViens.FirstOrDefault(x => x.MaNhanVien == nv.MaNhanVien);
            unv.TenNhanVien = nv.TenNhanVien;
            unv.MaRap = nv.MaRap;
            unv.GioiTinh = nv.GioiTinh;
            unv.MatKhau = Encode(nv.MatKhau);
            unv.NgaySinh = nv.NgaySinh;
            unv.NgayVaoLam = nv.NgayVaoLam;
            unv.SoCMND = nv.SoCMND;
            unv.DiaChi = nv.DiaChi;
            unv.DienThoai = nv.DienThoai;
            unv.Email = nv.Email;
            unv.ChucVu = nv.ChucVu;

            if (uploadhinh != null && uploadhinh.ContentLength > 0) {

                string id = nv.MaNhanVien;
                string _FileName = "";

                _FileName = uploadhinh.FileName;
                string _path = Path.Combine(Server.MapPath("~/Public/images/NhanVien"), _FileName);
                uploadhinh.SaveAs(_path);

                unv.Anh = _FileName;


            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult dsNhanVien()
        {
            try {
                var dsNV = (from da in db.NhanViens
                            select new {
                                MaNhanVien = da.MaNhanVien,
                                TenNhanVien = da.TenNhanVien,
                                MaRap = da.MaRap,
                                DiaChi = da.DiaChi,
                                DienThoai = da.DienThoai,
                                Email = da.Email,
                                NgaySinh = da.NgaySinh,
                                GioiTinh = da.GioiTinh,
                                ChucVu = da.ChucVu,
                                NgayVaoLam = da.NgayVaoLam,
                                Anh = da.Anh,
                                mk = da.MatKhau,
                                CCCD = da.SoCMND
                            }).ToList();
                return Json(new { code = 200, dsNV = dsNV }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(new {
                    code = 500,
                    msg = "Lấy dữ liệu thất bại" + ex.Message
                }, JsonRequestBehavior.AllowGet);

            }
        }

        //[HttpPost]
        //public ActionResult addNhanVien(string manv, string tennv, string diachi, string sdt, string email, string mk, string cccd, 
        //    DateTime ngaysinh, string gioitinh,DateTime ngayvaolam,string chucvu, string marap,string anh, float hsl, HttpPostedFileBase uploadhinh)
        //{
        //    try
        //    {
        //        var l = new NhanVien();
        //        l.MaNhanVien = manv;
        //        l.TenNhanVien = tennv;
        //        l.DiaChi = diachi;
        //        l.DienThoai = sdt;
        //        l.Email = email;
        //        l.MatKhau = mk;
        //        l.SoCMND = cccd;
        //        l.NgaySinh = ngaysinh;
        //        l.GioiTinh = gioitinh;
        //        l.NgayVaoLam = ngayvaolam;
        //        l.MaRap = marap;
        //        l.ChucVu = chucvu;
        //        //l.Anh = anh;
        //        l.HSL = hsl;

        //    if (uploadhinh != null && uploadhinh.ContentLength > 0)
        //    {

        //        string id = l.MaNhanVien;
        //        string _FileName = "";

        //        _FileName = uploadhinh.FileName;
        //        string _path = Path.Combine(Server.MapPath("~/Public/images/NhanVien"), _FileName);
        //        uploadhinh.SaveAs(_path);

        //        l.Anh = _FileName;
        //            anh=_FileName;
        //        l.Anh = anh;
        //        }
        //        // db.SaveChanges();

        //        db.NhanViens.Add(l);
        //        db.SaveChanges();

        //        return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
        //    }




        //}
        [HttpGet]
        public ActionResult chiTietNV(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.NhanViens.SingleOrDefault(x => x.MaNhanVien == id);
            return Json(new { code = 200, da = da }, JsonRequestBehavior.AllowGet);

        }
        //[HttpPost]
        //public ActionResult editNhanVien(string manv, string tennv, string diachi, string sdt, string email, string mk, string cccd, DateTime ngaysinh, string gioitinh,
        //    DateTime ngayvaolam, string chucvu, string marap, string anh, float hsl, HttpPostedFileBase uploadhinh)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var da = db.NhanViens.SingleOrDefault(x => x.MaNhanVien == manv);

        //    da.TenNhanVien = tennv;
        //    da.DiaChi = diachi;
        //    da.DienThoai = sdt;
        //    da.GioiTinh = gioitinh;
        //    da.NgaySinh = ngaysinh;
        //    da.MatKhau = mk;
        //    da.SoCMND = cccd;
        //    da.Email = email;
        //    da.NgayVaoLam = ngayvaolam;
        //    da.ChucVu = chucvu;
        //    da.MaRap = marap;
        //    //da.Anh = anh;
        //    da.HSL = hsl;
        //    if (uploadhinh != null && uploadhinh.ContentLength > 0)
        //    {

        //        string id = da.MaNhanVien;
        //        string _FileName = "";

        //        _FileName = uploadhinh.FileName;
        //        string _path = Path.Combine(Server.MapPath("~/Public/images/NhanVien"), _FileName);
        //        uploadhinh.SaveAs(_path);

        //        da.Anh = _FileName;
        //        anh= _FileName;


        //    }
        //    db.SaveChanges();
        //    return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        //    //return View("Index");
        //}
        [HttpPost]
        public ActionResult xoaNhanVien(string id)
        {

            NhanVien da = db.NhanViens.Find(id);
            db.NhanViens.Remove(da);
            db.SaveChanges();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        public string Encode(string s)
        {
            string str = "";
            Byte[] buffer = System.Text.Encoding.UTF8.GetBytes(s);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (Byte b in buffer) {
                str += b.ToString("X2");
            }
            return str;
        }

    }
}
