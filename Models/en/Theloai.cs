namespace BaiTapLon_Web.Models.en
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Theloai")]
    public partial class Theloai
    {
        [Key]
        [StringLength(10)]
        public string MaTheLoai { get; set; }

        [StringLength(50)]
        public string TenTheLoai { get; set; }
    }
}
