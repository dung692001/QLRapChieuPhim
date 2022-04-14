namespace BaiTapLon_Web.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoAn")]
    public partial class DoAn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DoAn()
        {
            CTHoaDonBanDoAns = new HashSet<CTHoaDonBanDoAn>();
        }

        [Key]
        [StringLength(10)]
        public string MaDoAn { get; set; }

        [StringLength(3)]
        public string Size { get; set; }

        [StringLength(50)]
        public string TenDoAn { get; set; }

        public double? Gia { get; set; }

        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHoaDonBanDoAn> CTHoaDonBanDoAns { get; set; }
    }
}
