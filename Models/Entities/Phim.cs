namespace BaiTapLon_Web.Models.Entities
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
        //[Required(ErrorMessage = "Hãy nhập mã phim.")]
        public string MaPhim { get; set; }
        
        [StringLength(100)]
        [Required(ErrorMessage = "Hãy nhập tên phim.")]
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

        [Required(ErrorMessage = "Hãy nhập ngày khởi chiếu.")]
        [Column(TypeName = "date")]
        public DateTime? NgayKhoiChieu { get; set; }

        [Required(ErrorMessage = "Hãy nhập ngày kết thúc.")]
        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        public double? TongChiPhi { get; set; }

        [StringLength(50)]
        public string Anh { get; set; }

        [Required(ErrorMessage = "Hãy nhập thời lượng.")]
        public int? ThoiLuong { get; set; }

        [Required(ErrorMessage = "Hãy nhập năm sản xuất.")]
        [Range(2000, 2022, ErrorMessage = "Giá trị phải là từ 2000 tới 2022.")]
        public int? NamSX { get; set; }

        public string Trailer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BuoiChieu> BuoiChieux { get; set; }

        public virtual HangSX HangSX { get; set; }

        public virtual NuocSanXuat NuocSanXuat { get; set; }

        public virtual Theloai Theloai { get; set; }
    }
}
