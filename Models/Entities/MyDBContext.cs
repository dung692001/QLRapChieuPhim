using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BaiTapLon_Web.Models.Entities
{
    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("name=MyDBContext")
        {
        }

        public virtual DbSet<BuoiChieu> BuoiChieux { get; set; }
        public virtual DbSet<CTHoaDonBanDoAn> CTHoaDonBanDoAns { get; set; }
        public virtual DbSet<DoAn> DoAns { get; set; }
        public virtual DbSet<HangSX> HangSXes { get; set; }
        public virtual DbSet<HoaDonBanDoAn> HoaDonBanDoAns { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NuocSanXuat> NuocSanXuats { get; set; }
        public virtual DbSet<Phim> Phims { get; set; }
        public virtual DbSet<PhongChieu> PhongChieux { get; set; }
        public virtual DbSet<RapPhim> RapPhims { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Theloai> Theloais { get; set; }
        public virtual DbSet<Ve> Ves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuoiChieu>()
                .Property(e => e.MaBuoiChieu)
                .IsFixedLength();

            modelBuilder.Entity<BuoiChieu>()
                .Property(e => e.MaPhim)
                .IsFixedLength();

            modelBuilder.Entity<BuoiChieu>()
                .Property(e => e.MaRap)
                .IsFixedLength();

            modelBuilder.Entity<BuoiChieu>()
                .Property(e => e.MaPhong)
                .IsFixedLength();

            modelBuilder.Entity<BuoiChieu>()
                .Property(e => e.GiaVe)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CTHoaDonBanDoAn>()
                .Property(e => e.MaHD)
                .IsFixedLength();

            modelBuilder.Entity<CTHoaDonBanDoAn>()
                .Property(e => e.MaDoAn)
                .IsFixedLength();

            modelBuilder.Entity<DoAn>()
                .Property(e => e.MaDoAn)
                .IsFixedLength();

            modelBuilder.Entity<DoAn>()
                .Property(e => e.Size)
                .IsFixedLength();

            modelBuilder.Entity<DoAn>()
                .HasMany(e => e.CTHoaDonBanDoAns)
                .WithRequired(e => e.DoAn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HangSX>()
                .Property(e => e.MaHangSX)
                .IsFixedLength();

            modelBuilder.Entity<HangSX>()
                .HasMany(e => e.Phims)
                .WithRequired(e => e.HangSX)
                .HasForeignKey(e => e.MaHang);

            modelBuilder.Entity<HoaDonBanDoAn>()
                .Property(e => e.MaHD)
                .IsFixedLength();

            modelBuilder.Entity<HoaDonBanDoAn>()
                .Property(e => e.MaNhanVien)
                .IsFixedLength();

            modelBuilder.Entity<HoaDonBanDoAn>()
                .HasMany(e => e.CTHoaDonBanDoAns)
                .WithRequired(e => e.HoaDonBanDoAn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
               .Property(e => e.MaKhachHang)
               .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoCMND)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.DienThoai)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNhanVien)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaRap)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SoCMND)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.DienThoai)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MatKhau)
                .IsFixedLength();

            modelBuilder.Entity<NuocSanXuat>()
                .Property(e => e.NuocSX)
                .IsFixedLength();

            modelBuilder.Entity<NuocSanXuat>()
                .HasMany(e => e.Phims)
                .WithRequired(e => e.NuocSanXuat)
                .HasForeignKey(e => e.MaNuocSX);

            modelBuilder.Entity<Phim>()
                .Property(e => e.MaPhim)
                .IsFixedLength();

            modelBuilder.Entity<Phim>()
                .Property(e => e.MaNuocSX)
                .IsFixedLength();

            modelBuilder.Entity<Phim>()
                .Property(e => e.MaHang)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<Phim>()
                .Property(e => e.MaTheLoai)
                .IsFixedLength();

            modelBuilder.Entity<Phim>()
                .Property(e => e.NoiDungChinh)
                .IsUnicode(false);

            modelBuilder.Entity<Phim>()
                .HasMany(e => e.BuoiChieux)
                .WithRequired(e => e.Phim)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhongChieu>()
                .Property(e => e.MaPhong)
                .IsFixedLength();

            modelBuilder.Entity<PhongChieu>()
                .Property(e => e.MaRap)
                .IsFixedLength();

            modelBuilder.Entity<PhongChieu>()
                .HasMany(e => e.BuoiChieux)
                .WithRequired(e => e.PhongChieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RapPhim>()
                .Property(e => e.MaRap)
                .IsFixedLength();

            modelBuilder.Entity<RapPhim>()
                .Property(e => e.DienThoai)
                .IsFixedLength();

            modelBuilder.Entity<RapPhim>()
                .HasMany(e => e.BuoiChieux)
                .WithRequired(e => e.RapPhim)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Theloai>()
                .Property(e => e.MaTheLoai)
                .IsFixedLength();

            modelBuilder.Entity<Ve>()
                .Property(e => e.MaVe)
                .IsFixedLength();

            modelBuilder.Entity<Ve>()
                .Property(e => e.MaBuoiChieu)
                .IsFixedLength();

            modelBuilder.Entity<Ve>()
                .Property(e => e.HangGhe)
                .IsFixedLength();

            modelBuilder.Entity<Ve>()
                .Property(e => e.TienVe)
                .HasPrecision(19, 4);
        }
    }
}
