namespace BaiTapLon_Web.Models.en
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HangSX")]
    public partial class HangSX
    {
        [Key]
        [StringLength(10)]
        public string MaHangSX { get; set; }

        [StringLength(50)]
        public string TenHangSX { get; set; }
    }
}
