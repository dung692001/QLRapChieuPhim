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
    public class KhachHangsController : Controller
    {
        private MyDBContext db = new MyDBContext();


        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult dsKhachHang()
        {
            try {
                var dsKH = (from da in db.KhachHangs
                            select new {
                                MaKhachHang = da.MaKhachHang,
                                TenKhachHang = da.TenKhachHang,
                                DiaChi = da.DiaChi,
                                DienThoai = da.DienThoai,
                                Email = da.Email,
                                NgaySinh = da.NgaySinh,
                                GioiTinh = da.GioiTinh,
                                mk = da.Password,
                                CCCD = da.SoCMND
                            }).ToList();

                //return View("Index", dsKH);
                return Json(new { dsKH = dsKH }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(new {
                    code = 500,
                    msg = "Lấy dữ liệu thất bại" + ex.Message
                }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public ActionResult addKhachHang(string tenkh, string diachi, string sdt, string email, string mk, string cccd, DateTime ngaysinh, string gioitinh)
        {
            try {
                var l = new KhachHang();
                string id_last = db.KhachHangs.ToList().Last().MaKhachHang;
                string tmp = id_last.Replace("KH", "");
                int a = 0;
                if (tmp[0] == '0') {
                    tmp = tmp.Replace("0", "");
                }
                a = int.Parse(tmp) + 1;
                if (a < 10) {
                    l.MaKhachHang = "KH0" + a.ToString();
                }
                else {
                    l.MaKhachHang = "KH" + a.ToString();
                }


                l.TenKhachHang = tenkh;
                l.DiaChi = diachi;
                l.DienThoai = sdt;
                l.Email = email;
                l.Password = mk;
                l.SoCMND = cccd;
                l.NgaySinh = ngaysinh;
                l.GioiTinh = gioitinh;
                db.KhachHangs.Add(l);
                db.SaveChanges();

                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch {
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }




        }
        [HttpGet]
        public ActionResult chiTietKH(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.KhachHangs.SingleOrDefault(x => x.MaKhachHang == id);
            return Json(new { code = 200, da = da }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult editKhachHang(string makh, string tenkh, string diachi, string sdt, string email, string mk, string cccd, DateTime ngaysinh, string gioitinh)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.KhachHangs.SingleOrDefault(x => x.MaKhachHang == makh);

            da.TenKhachHang = tenkh;
            da.DiaChi = diachi;
            da.DienThoai = sdt;
            da.GioiTinh = gioitinh;
            da.NgaySinh = ngaysinh;
            da.Password = mk;
            da.SoCMND = cccd;
            da.Email = email;
            db.SaveChanges();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            //return View("Index");
        }
        [HttpPost]
        public ActionResult xoaKhachHang(string id)
        {

            KhachHang da = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(da);
            db.SaveChanges();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);


        }
    }
}
