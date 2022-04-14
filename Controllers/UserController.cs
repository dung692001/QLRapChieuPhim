using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using BaiTapLon_Web.Models.Entities;

namespace BaiTapLon_Web.Controllers
{
    public class UserController : Controller
    {
        MyDBContext db = new MyDBContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KhachHang user)
        {
            try {
                if (ModelState.IsValid) {
                    string macuoi = db.KhachHangs.ToList().Last().MaKhachHang;
                    string tmp = macuoi.Replace("KH", "");
                    int a = 0;
                    if (tmp[0] == '0') {
                        tmp = tmp.Replace("0", "");
                    }
                    a = int.Parse(tmp) + 1;
                    if (a < 10)
                        user.MaKhachHang = "KH0" + a.ToString();
                    else
                        user.MaKhachHang = "KH" + a.ToString();


                    var check = db.KhachHangs.FirstOrDefault(s => s.Email == user.Email);
                    if (check == null) {
                        db.Configuration.ValidateOnSaveEnabled = false;
                        //Check password vs confirm password khớp
                        if (user.Password == user.Password) {
                            //Mã hóa password
                            user.Password = this.Encode(user.Password);
                            db.KhachHangs.Add(user);
                            db.SaveChanges();
                            //dki thanh cong chuyen sang page login
                            return RedirectToAction("DangNhap");
                        }
                        else {

                            ViewBag.error = "Mật khẩu không khớp";
                            return View();
                        }

                    }
                    else {
                        ViewBag.error = "Email already exists! Use another email please!";
                        return View();
                    }
                }
                return View();
            }
            catch {
                ViewBag.error = "Mã khách hàng bị trùng";
                return View();
            }


        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(KhachHang input)
        {
            //Check email password
            string encode = Encode(input.Password);
            var user = db.KhachHangs.FirstOrDefault(x => x.Email == input.Email && x.Password == encode);
            if (user == null) {
                ViewBag.error = "Đăng nhập thất bại";
                return View();
            }
            //Đăng nhập thành công
            else {
                //Xử lí coockie lưu lại thông tin người dùng
                //Format: MAKH|TenKH vd: KH01|Hiep
                FormsAuthentication.SetAuthCookie((user.MaKhachHang + "|" + user.TenKhachHang), true);
                return RedirectToAction("Index", "BaiTapLon");
            }

        }

        //Đăng xuất
        public ActionResult DangXuat()
        {
            //Xử lí xóa cookie
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "BaiTapLon");
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