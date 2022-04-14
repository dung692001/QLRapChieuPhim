namespace BaiTapLon_Web.Models.en
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phim")]
    public partial class Phim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phim()
        {
            BuoiChieux = new HashSet<BuoiChieu>();
        }

        [Key]
        [StringLength(10)]
        public string MaPhim { get; set; }

        [Required]
        [StringLength(100)]
        public string TenPhim { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNuocSX { get; set; }

        [Required]
        [StringLength(10)]
        public string MaHang { get; set; }

        [Required]
        [StringLength(10)]
        public string MaTheLoai { get; set; }

        [StringLength(50)]
        public string DaoDien { get; set; }

        [StringLength(50)]
        public string NuDienVienChinh { get; set; }

        [StringLength(50)]
        public string NamDienVienChinh { get; set; }

        [Column(TypeName = "text")]
        public string NoiDungChinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKhoiChieu { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        public double? TongChiPhi { get; set; }

        [StringLength(250)]
        public string Anh { get; set; }

        public int? ThoiLuong { get; set; }

        public int? NamSX { get; set; }

        public string Trailer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BuoiChieu> BuoiChieux { get; set; }
    }
}
