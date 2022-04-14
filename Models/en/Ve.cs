namespace BaiTapLon_Web.Models.en
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ve")]
    public partial class Ve
    {
        [Key]
        [StringLength(10)]
        public string MaVe { get; set; }

        [Required]
        [StringLength(10)]
        public string MaBuoiChieu { get; set; }

        [Required]
        [StringLength(10)]
        public string HangGhe { get; set; }

        public int SoGhe { get; set; }

        [StringLength(25)]
        public string LoaiGhe { get; set; }

        [Column(TypeName = "money")]
        public decimal? TienVe { get; set; }

        public virtual BuoiChieu BuoiChieu { get; set; }
    }
}
