namespace BaiTapLon_Web.Models.en
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RapPhim")]
    public partial class RapPhim
    {
        [Key]
        [StringLength(10)]
        public string MaRap { get; set; }

        [StringLength(50)]
        public string TenRap { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        [StringLength(10)]
        public string DienThoai { get; set; }

        public int? SoPhong { get; set; }

        public int? TongSoGhe { get; set; }
    }
}
