namespace BaiTapLon_Web.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BuoiChieu")]
    public partial class BuoiChieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BuoiChieu()
        {
            Ves = new HashSet<Ve>();
        }
        
        [Key]
        [StringLength(10)]
        //[Required(ErrorMessage = "Hãy nhập mã buổi chiếu.")]
        public string MaBuoiChieu { get; set; }

        [Required]
        [StringLength(10)]
        public string MaPhim { get; set; }

        [Required]
        [StringLength(10)]
        public string MaRap { get; set; }

        [Required]
        [StringLength(10)]
        public string MaPhong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayChieu { get; set; }

        [Required(ErrorMessage = "Hãy nhập giờ chiếu.")]
        [Range(00, 24, ErrorMessage = "Giá trị phải là từ 00 tới 23.")]
        public int? GioChieu { get; set; }

        [Column(TypeName = "money")]    
        public decimal? GiaVe { get; set; }

        public int? SoVeDaBan { get; set; }

        public double? TongTien { get; set; }

        public virtual Phim Phim { get; set; }

        public virtual PhongChieu PhongChieu { get; set; }

        public virtual RapPhim RapPhim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ve> Ves { get; set; }
    }
}
