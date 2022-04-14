namespace BaiTapLon_Web.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RapPhim")]
    public partial class RapPhim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RapPhim()
        {
            BuoiChieux = new HashSet<BuoiChieu>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BuoiChieu> BuoiChieux { get; set; }
    }
}
