using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BaiTapLon_Web.Models.Entities;
using System.Security.Cryptography;
namespace BaiTapLon_Web.Controllers
{
    public class QuanLyController : Controller
    {
        
        MyDBContext db = new MyDBContext();
        // GET: QuanLy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(NhanVien input)
        {
            //Check email password
            string encode = Encode(input.MatKhau);
            var admin = db.NhanViens.FirstOrDefault(x => x.Email == input.Email && x.MatKhau == encode);
            if (admin == null) {
                ViewBag.error = "Đăng nhập thất bại";
                return View();
            }
            //Đăng nhập thành công
            else {
                //Xử lí coockie lưu lại thông tin người dùng
                //Format: MAKH|TenKH vd: KH01|Hiep
                FormsAuthentication.SetAuthCookie((admin.MaNhanVien + "|" + admin.TenNhanVien), true);
                return RedirectToAction("Index", "Admin");
            }

        }

        /// <summary>
        /// Hàm mã hóa password MD5 : 1 chiều
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
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