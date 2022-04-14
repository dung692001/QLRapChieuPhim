namespace BaiTapLon_Web.Models.en
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NuocSanXuat")]
    public partial class NuocSanXuat
    {
        [Key]
        [StringLength(10)]
        public string NuocSX { get; set; }

        [StringLength(50)]
        public string TenNuocSX { get; set; }
    }
}
