using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiTapLon_Web.CurrentUser
{
    public class CurrentUser
    {
        public static string MaKH {
            get {
                try {
                    var strInfo = HttpContext.Current.User.Identity.Name.Split('|');
                    return strInfo[0];
                }
                catch {
                    return "";
                }
            }
        }

        public static string TenKhachHang {
            get {
                try {
                    var strInfo = HttpContext.Current.User.Identity.Name.Split('|');
                    return strInfo[1];
                }
                catch {
                    return "";
                }
            }
        }
    }
}