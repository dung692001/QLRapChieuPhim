using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BaiTapLon_Web.Controllers
{
    public class MoviesController : ApiController
    {
        //Dịch vụ lấy phim
        [HttpGet]

        public List<Phimm> GetMovieLists()

        {

            DBMoviesDataContext dbMovie = new DBMoviesDataContext();
            return dbMovie.Phimx.ToList();

        }


    }
}