using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BaiTapLon_Web.Controllers
{
    public class ScreeningsController : ApiController
    {
        //1. Dịch vụ lấy toan bo buoi chieu
        [HttpGet]

        public List<BuoiChieu> GetRoomLists()

        {

            DBScreeningsDataContext dBScreenings = new DBScreeningsDataContext();

            return dBScreenings.BuoiChieus.ToList();

        }

        //2. Dịch vụ lấy buoi chieu theo ma phong va phim

        [HttpGet]

        public List<BuoiChieu> GetRoom(string movieID, string roomID)

        {

            DBScreeningsDataContext dBScreenings = new DBScreeningsDataContext();

            return (from s in dBScreenings.BuoiChieus where s.MaPhim.Contains(movieID) && s.MaPhong.Contains(roomID) select s).ToList();

        }
    }
}