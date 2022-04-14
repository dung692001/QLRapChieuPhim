using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BaiTapLon_Web.Controllers
{
    public class RoomsController : ApiController
    {
        //Dịch vụ lấy phong chieu
        [HttpGet]

        public List<PhongChieu> GetRoomLists()

        {

            DBRoomsDataContext dbRoom = new DBRoomsDataContext();

            return dbRoom.PhongChieus.ToList();

        }
    }
}