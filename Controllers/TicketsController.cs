using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BaiTapLon_Web.Controllers
{
    public class TicketsController : ApiController
    {

        // Dịch vụ lấy thông tin ve voi ma buoi chieu

        [HttpGet]

        public List<Ve> GetRoom(string screeningID)

        {

            DBTicketsDataContext dBTickets = new DBTicketsDataContext();

            return (from s in dBTickets.Ves where (s.MaBuoiChieu == screeningID) select s).ToList();

        }

        // httpPost, dịch vụ thêm mới ve
        [HttpPost]

        public bool InsertNewTicket(string id, string bcId, string row, int seat, string type, decimal cost)

        {

            try {

                DBTicketsDataContext dBTickets = new

                DBTicketsDataContext();

                Ve ticket = new Ve(); ticket.MaVe = id; ticket.MaBuoiChieu = bcId; ticket.HangGhe = row; ticket.SoGhe = seat; ticket.LoaiGhe = type; ticket.TienVe = cost;



                dBTickets.Ves.InsertOnSubmit(ticket);

                dBTickets.SubmitChanges(); return true;

            }

            catch {

                return false;

            }

        }


    }
}