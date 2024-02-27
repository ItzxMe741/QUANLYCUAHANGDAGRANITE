using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CUAHANGBANDAGRANITE.Models;

public partial class QlchdgraniteContext : DbContext
{
    public QlchdgraniteContext()
    {
    }

    public QlchdgraniteContext(DbContextOptions<QlchdgraniteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitietnhap> Chitietnhaps { get; set; }

    public virtual DbSet<Chitietxuat> Chitietxuats { get; set; }

    public virtual DbSet<Dagranite> Dagranites { get; set; }

    public virtual DbSet<Donvivanchuyen> Donvivanchuyens { get; set; }

    public virtual DbSet<Hinhthucthanhtoan> Hinhthucthanhtoans { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Loaidum> Loaida { get; set; }

    public virtual DbSet<Loaithung> Loaithungs { get; set; }

    public virtual DbSet<Nganhang> Nganhangs { get; set; }

    public virtual DbSet<NganhangKhachhang> NganhangKhachhangs { get; set; }

    public virtual DbSet<NganhangNhacungcap> NganhangNhacungcaps { get; set; }

    public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Noidungungdung> Noidungungdungs { get; set; }

    public virtual DbSet<Phieunhapkho> Phieunhapkhos { get; set; }

    public virtual DbSet<Phieuthanhtoan> Phieuthanhtoans { get; set; }

    public virtual DbSet<Phieuthutien> Phieuthutiens { get; set; }

    public virtual DbSet<Phieuxuatkho> Phieuxuatkhos { get; set; }

    public virtual DbSet<Quycachdonggoi> Quycachdonggois { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Thongtinxe> Thongtinxes { get; set; }

    public virtual DbSet<Trangthai> Trangthais { get; set; }

    public virtual DbSet<Ungdung> Ungdungs { get; set; }

    public virtual DbSet<Xe> Xes { get; set; }

    public virtual DbSet<Xuatxu> Xuatxus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=QLCHDGRANITE;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitietnhap>(entity =>
        {
            entity.HasKey(e => e.Idctn).HasName("PK__CHITIETN__91A8964E6A24C88D");

            entity.ToTable("CHITIETNHAP");

            entity.Property(e => e.Idctn).HasColumnName("IDCTN");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Dongianhap).HasColumnName("DONGIANHAP");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.Loaithungidloaithung).HasColumnName("LOAITHUNGIDLOAITHUNG");
            entity.Property(e => e.Phieunhapkhoidpnk).HasColumnName("PHIEUNHAPKHOIDPNK");
            entity.Property(e => e.Sothung).HasColumnName("SOTHUNG");
            entity.Property(e => e.Vat).HasColumnName("VAT");
            entity.Property(e => e.Xacnhan).HasColumnName("XACNHAN");

            entity.HasOne(d => d.LoaithungidloaithungNavigation).WithMany(p => p.Chitietnhaps)
                .HasForeignKey(d => d.Loaithungidloaithung)
                .HasConstraintName("FK_LT_IDLOAITHUNG");

            entity.HasOne(d => d.PhieunhapkhoidpnkNavigation).WithMany(p => p.Chitietnhaps)
                .HasForeignKey(d => d.Phieunhapkhoidpnk)
                .HasConstraintName("FK_CTN_IDPNK");
        });

        modelBuilder.Entity<Chitietxuat>(entity =>
        {
            entity.HasKey(e => e.Idctx).HasName("PK__CHITIETX__91A897B02C5EEC24");

            entity.ToTable("CHITIETXUAT");

            entity.Property(e => e.Idctx).HasColumnName("IDCTX");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Dientichkhdat).HasColumnName("DIENTICHKHDAT");
            entity.Property(e => e.Dongiaxuat).HasColumnName("DONGIAXUAT");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.Loaithungidloaithung).HasColumnName("LOAITHUNGIDLOAITHUNG");
            entity.Property(e => e.Phieuxuatkhoidpxk).HasColumnName("PHIEUXUATKHOIDPXK");
            entity.Property(e => e.Sothunggiaokh).HasColumnName("SOTHUNGGIAOKH");
            entity.Property(e => e.Vat).HasColumnName("VAT");
            entity.Property(e => e.Xacnhan).HasColumnName("XACNHAN");

            entity.HasOne(d => d.LoaithungidloaithungNavigation).WithMany(p => p.Chitietxuats)
                .HasForeignKey(d => d.Loaithungidloaithung)
                .HasConstraintName("FK_CTX_IDLOAITHUNG");

            entity.HasOne(d => d.PhieuxuatkhoidpxkNavigation).WithMany(p => p.Chitietxuats)
                .HasForeignKey(d => d.Phieuxuatkhoidpxk)
                .HasConstraintName("FK_CTX_IDPXK");
        });

        modelBuilder.Entity<Dagranite>(entity =>
        {
            entity.HasKey(e => e.Idda).HasName("PK__DAGRANIT__B87DB881C90ACC23");

            entity.ToTable("DAGRANITE");

            entity.Property(e => e.Idda).HasColumnName("IDDA");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Chieudai).HasColumnName("CHIEUDAI");
            entity.Property(e => e.Chieurong).HasColumnName("CHIEURONG");
            entity.Property(e => e.Dientichbemat).HasColumnName("DIENTICHBEMAT");
            entity.Property(e => e.Dvt)
                .HasMaxLength(255)
                .HasColumnName("DVT");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Image).HasColumnName("IMAGE");
            entity.Property(e => e.Khoiluong).HasColumnName("KHOILUONG");
            entity.Property(e => e.Loaidaidloai).HasColumnName("LOAIDAIDLOAI");
            entity.Property(e => e.Mada)
                .HasMaxLength(255)
                .HasColumnName("MADA");
            entity.Property(e => e.Tenda)
                .HasMaxLength(255)
                .HasColumnName("TENDA");
            entity.Property(e => e.Tengoikhac)
                .HasMaxLength(255)
                .HasColumnName("TENGOIKHAC");
            entity.Property(e => e.Xuatxuidxuatxu).HasColumnName("XUATXUIDXUATXU");

            entity.HasOne(d => d.LoaidaidloaiNavigation).WithMany(p => p.Dagranites)
                .HasForeignKey(d => d.Loaidaidloai)
                .HasConstraintName("FK_DA_IDLOAI");

            entity.HasOne(d => d.XuatxuidxuatxuNavigation).WithMany(p => p.Dagranites)
                .HasForeignKey(d => d.Xuatxuidxuatxu)
                .HasConstraintName("FK_DA_IDXUATXU");
        });

        modelBuilder.Entity<Donvivanchuyen>(entity =>
        {
            entity.HasKey(e => e.Iddvvc).HasName("PK__DONVIVAN__303C0973A95ABA19");

            entity.ToTable("DONVIVANCHUYEN");

            entity.Property(e => e.Iddvvc).HasColumnName("IDDVVC");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Diachi)
                .HasColumnType("ntext")
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Image).HasColumnName("IMAGE");
            entity.Property(e => e.Madvvc)
                .HasMaxLength(255)
                .HasColumnName("MADVVC");
            entity.Property(e => e.Masothue)
                .HasMaxLength(255)
                .HasColumnName("MASOTHUE");
            entity.Property(e => e.Sdt)
                .HasMaxLength(255)
                .HasColumnName("SDT");
            entity.Property(e => e.Taikhoanidtaikhoan).HasColumnName("TAIKHOANIDTAIKHOAN");
            entity.Property(e => e.Tendvvc)
                .HasMaxLength(255)
                .HasColumnName("TENDVVC");

            entity.HasOne(d => d.TaikhoanidtaikhoanNavigation).WithMany(p => p.Donvivanchuyens)
                .HasForeignKey(d => d.Taikhoanidtaikhoan)
                .HasConstraintName("FK_DVVC_IDTAIKHOAN");
        });

        modelBuilder.Entity<Hinhthucthanhtoan>(entity =>
        {
            entity.HasKey(e => e.Idhttt).HasName("PK__HINHTHUC__9DCEA6E3339EFB59");

            entity.ToTable("HINHTHUCTHANHTOAN");

            entity.Property(e => e.Idhttt).HasColumnName("IDHTTT");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Mahttt)
                .HasMaxLength(255)
                .HasColumnName("MAHTTT");
            entity.Property(e => e.Nganhangidnganhang).HasColumnName("NGANHANGIDNGANHANG");
            entity.Property(e => e.Tenhttt)
                .HasMaxLength(255)
                .HasColumnName("TENHTTT");

            entity.HasOne(d => d.NganhangidnganhangNavigation).WithMany(p => p.Hinhthucthanhtoans)
                .HasForeignKey(d => d.Nganhangidnganhang)
                .HasConstraintName("FK_HTTT_IDNGANHANG");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.Idkhachhang).HasName("PK__KHACHHAN__629294BE2790050C");

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.Idkhachhang).HasColumnName("IDKHACHHANG");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Diachi)
                .HasColumnType("ntext")
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Image).HasColumnName("IMAGE");
            entity.Property(e => e.Makhachhang)
                .HasMaxLength(255)
                .HasColumnName("MAKHACHHANG");
            entity.Property(e => e.Masothue)
                .HasMaxLength(255)
                .HasColumnName("MASOTHUE");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("date")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(255)
                .HasColumnName("SDT");
            entity.Property(e => e.Taikhoanidtaikhoan).HasColumnName("TAIKHOANIDTAIKHOAN");
            entity.Property(e => e.Tenkhachhang)
                .HasMaxLength(255)
                .HasColumnName("TENKHACHHANG");

            entity.HasOne(d => d.TaikhoanidtaikhoanNavigation).WithMany(p => p.Khachhangs)
                .HasForeignKey(d => d.Taikhoanidtaikhoan)
                .HasConstraintName("FK_KH_IDTAIKHOAN");
        });

        modelBuilder.Entity<Loaidum>(entity =>
        {
            entity.HasKey(e => e.Idloai).HasName("PK__LOAIDA__0CDEF5196EEC577E");

            entity.ToTable("LOAIDA");

            entity.Property(e => e.Idloai).HasColumnName("IDLOAI");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Maloai)
                .HasMaxLength(255)
                .HasColumnName("MALOAI");
            entity.Property(e => e.Tenloai)
                .HasMaxLength(255)
                .HasColumnName("TENLOAI");
        });

        modelBuilder.Entity<Loaithung>(entity =>
        {
            entity.HasKey(e => e.Idloaithung).HasName("PK__LOAITHUN__365845F66E16B5E7");

            entity.ToTable("LOAITHUNG");

            entity.Property(e => e.Idloaithung).HasColumnName("IDLOAITHUNG");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Dagraniteidda).HasColumnName("DAGRANITEIDDA");
            entity.Property(e => e.Dongiaban).HasColumnName("DONGIABAN");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Maloaithung)
                .HasMaxLength(255)
                .HasColumnName("MALOAITHUNG");
            entity.Property(e => e.Quycachidquycach).HasColumnName("QUYCACHIDQUYCACH");
            entity.Property(e => e.Sothungcon).HasColumnName("SOTHUNGCON");
            entity.Property(e => e.Tenloaithung)
                .HasMaxLength(255)
                .HasColumnName("TENLOAITHUNG");

            entity.HasOne(d => d.DagraniteiddaNavigation).WithMany(p => p.Loaithungs)
                .HasForeignKey(d => d.Dagraniteidda)
                .HasConstraintName("FK_LT_IDDA");

            entity.HasOne(d => d.QuycachidquycachNavigation).WithMany(p => p.Loaithungs)
                .HasForeignKey(d => d.Quycachidquycach)
                .HasConstraintName("FK_LT_IDQUYCACH");
        });

        modelBuilder.Entity<Nganhang>(entity =>
        {
            entity.HasKey(e => e.Idnganhang).HasName("PK__NGANHANG__4C4BF656B7A262EB");

            entity.ToTable("NGANHANG");

            entity.Property(e => e.Idnganhang).HasColumnName("IDNGANHANG");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Manganhang)
                .HasMaxLength(255)
                .HasColumnName("MANGANHANG");
            entity.Property(e => e.Tennganhang)
                .HasMaxLength(255)
                .HasColumnName("TENNGANHANG");
        });

        modelBuilder.Entity<NganhangKhachhang>(entity =>
        {
            entity.HasKey(e => e.IdnhKh).HasName("PK__NGANHANG__5A4319A57D8DA1E8");

            entity.ToTable("NGANHANG_KHACHHANG");

            entity.Property(e => e.IdnhKh).HasColumnName("IDNH_KH");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Khachhangidkhachhang).HasColumnName("KHACHHANGIDKHACHHANG");
            entity.Property(e => e.Nganhangidnganhang).HasColumnName("NGANHANGIDNGANHANG");
            entity.Property(e => e.Sotaikhoan)
                .HasMaxLength(255)
                .HasColumnName("SOTAIKHOAN");

            entity.HasOne(d => d.KhachhangidkhachhangNavigation).WithMany(p => p.NganhangKhachhangs)
                .HasForeignKey(d => d.Khachhangidkhachhang)
                .HasConstraintName("FK_NH_KH_IDKHACHHANG");

            entity.HasOne(d => d.NganhangidnganhangNavigation).WithMany(p => p.NganhangKhachhangs)
                .HasForeignKey(d => d.Nganhangidnganhang)
                .HasConstraintName("FK_NH_KH_IDNGANHANG");
        });

        modelBuilder.Entity<NganhangNhacungcap>(entity =>
        {
            entity.HasKey(e => e.IdnhNcc).HasName("PK__NGANHANG__2049BE4AEE5CDADD");

            entity.ToTable("NGANHANG_NHACUNGCAP");

            entity.Property(e => e.IdnhNcc).HasColumnName("IDNH_NCC");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Nganhangidnganhang).HasColumnName("NGANHANGIDNGANHANG");
            entity.Property(e => e.Nhacungcapidncc).HasColumnName("NHACUNGCAPIDNCC");
            entity.Property(e => e.Sotaikhoan)
                .HasMaxLength(255)
                .HasColumnName("SOTAIKHOAN");

            entity.HasOne(d => d.NganhangidnganhangNavigation).WithMany(p => p.NganhangNhacungcaps)
                .HasForeignKey(d => d.Nganhangidnganhang)
                .HasConstraintName("FK_NH_NCC_IDNGANHANG");

            entity.HasOne(d => d.NhacungcapidnccNavigation).WithMany(p => p.NganhangNhacungcaps)
                .HasForeignKey(d => d.Nhacungcapidncc)
                .HasConstraintName("FK_NH_NCC_IDNCC");
        });

        modelBuilder.Entity<Nhacungcap>(entity =>
        {
            entity.HasKey(e => e.Idncc).HasName("PK__NHACUNGC__945E47057CBFA4A7");

            entity.ToTable("NHACUNGCAP");

            entity.Property(e => e.Idncc).HasColumnName("IDNCC");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Diachi)
                .HasColumnType("ntext")
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Image).HasColumnName("IMAGE");
            entity.Property(e => e.Mancc)
                .HasMaxLength(255)
                .HasColumnName("MANCC");
            entity.Property(e => e.Masothue)
                .HasMaxLength(255)
                .HasColumnName("MASOTHUE");
            entity.Property(e => e.Sdt)
                .HasMaxLength(255)
                .HasColumnName("SDT");
            entity.Property(e => e.Taikhoanidtaikhoan).HasColumnName("TAIKHOANIDTAIKHOAN");
            entity.Property(e => e.Tenncc)
                .HasMaxLength(255)
                .HasColumnName("TENNCC");

            entity.HasOne(d => d.TaikhoanidtaikhoanNavigation).WithMany(p => p.Nhacungcaps)
                .HasForeignKey(d => d.Taikhoanidtaikhoan)
                .HasConstraintName("FK_NCC_IDTAIKHOAN");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Idnhanvien).HasName("PK__NHANVIEN__CEEFA9839DAD5690");

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.Idnhanvien).HasColumnName("IDNHANVIEN");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Image).HasColumnName("IMAGE");
            entity.Property(e => e.Manhanvien)
                .HasMaxLength(255)
                .HasColumnName("MANHANVIEN");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("date")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(255)
                .HasColumnName("SDT");
            entity.Property(e => e.Taikhoanidtaikhoan).HasColumnName("TAIKHOANIDTAIKHOAN");
            entity.Property(e => e.Tennhanvien)
                .HasMaxLength(255)
                .HasColumnName("TENNHANVIEN");

            entity.HasOne(d => d.TaikhoanidtaikhoanNavigation).WithMany(p => p.Nhanviens)
                .HasForeignKey(d => d.Taikhoanidtaikhoan)
                .HasConstraintName("FK_NV_IDTAIKHOAN");
        });

        modelBuilder.Entity<Noidungungdung>(entity =>
        {
            entity.HasKey(e => e.Idndud).HasName("PK__NOIDUNGU__383E0020C6B91556");

            entity.ToTable("NOIDUNGUNGDUNG");

            entity.Property(e => e.Idndud).HasColumnName("IDNDUD");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Dagraniteidda).HasColumnName("DAGRANITEIDDA");
            entity.Property(e => e.Ungdungidungdung).HasColumnName("UNGDUNGIDUNGDUNG");

            entity.HasOne(d => d.DagraniteiddaNavigation).WithMany(p => p.Noidungungdungs)
                .HasForeignKey(d => d.Dagraniteidda)
                .HasConstraintName("FK_NDUD_IDDA");

            entity.HasOne(d => d.UngdungidungdungNavigation).WithMany(p => p.Noidungungdungs)
                .HasForeignKey(d => d.Ungdungidungdung)
                .HasConstraintName("FK_NDUD_IDUNGDUNG");
        });

        modelBuilder.Entity<Phieunhapkho>(entity =>
        {
            entity.HasKey(e => e.Idpnk).HasName("PK__PHIEUNHA__98FEDC48E3B526CB");

            entity.ToTable("PHIEUNHAPKHO");

            entity.Property(e => e.Idpnk).HasColumnName("IDPNK");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Donvivanchuyeniddvvc).HasColumnName("DONVIVANCHUYENIDDVVC");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Ngaydat)
                .HasColumnType("date")
                .HasColumnName("NGAYDAT");
            entity.Property(e => e.Ngaygiaohang)
                .HasColumnType("date")
                .HasColumnName("NGAYGIAOHANG");
            entity.Property(e => e.Ngaytiepnhan)
                .HasColumnType("date")
                .HasColumnName("NGAYTIEPNHAN");
            entity.Property(e => e.Nhacungcapidncc).HasColumnName("NHACUNGCAPIDNCC");
            entity.Property(e => e.Nhanvienidnhanvien).HasColumnName("NHANVIENIDNHANVIEN");
            entity.Property(e => e.Sopnk)
                .HasMaxLength(255)
                .HasColumnName("SOPNK");
            entity.Property(e => e.Sotiendathanhtoan).HasColumnName("SOTIENDATHANHTOAN");
            entity.Property(e => e.Tongtien).HasColumnName("TONGTIEN");
            entity.Property(e => e.Trangthaiidtt).HasColumnName("TRANGTHAIIDTT");

            entity.HasOne(d => d.DonvivanchuyeniddvvcNavigation).WithMany(p => p.Phieunhapkhos)
                .HasForeignKey(d => d.Donvivanchuyeniddvvc)
                .HasConstraintName("FK_PNK_IDDVVC");

            entity.HasOne(d => d.NhacungcapidnccNavigation).WithMany(p => p.Phieunhapkhos)
                .HasForeignKey(d => d.Nhacungcapidncc)
                .HasConstraintName("FK_PNK_IDNCC");

            entity.HasOne(d => d.NhanvienidnhanvienNavigation).WithMany(p => p.Phieunhapkhos)
                .HasForeignKey(d => d.Nhanvienidnhanvien)
                .HasConstraintName("FK_PNK_IDNHANVIEN");

            entity.HasOne(d => d.TrangthaiidttNavigation).WithMany(p => p.Phieunhapkhos)
                .HasForeignKey(d => d.Trangthaiidtt)
                .HasConstraintName("FK_PNK_IDTT");
        });

        modelBuilder.Entity<Phieuthanhtoan>(entity =>
        {
            entity.HasKey(e => e.Idptt).HasName("PK__PHIEUTHA__98FA3DCE48E21FC4");

            entity.ToTable("PHIEUTHANHTOAN");

            entity.Property(e => e.Idptt).HasColumnName("IDPTT");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Htttidhttt).HasColumnName("HTTTIDHTTT");
            entity.Property(e => e.Ngaythanhtoan)
                .HasColumnType("date")
                .HasColumnName("NGAYTHANHTOAN");
            entity.Property(e => e.Nhanvienidnhanvien).HasColumnName("NHANVIENIDNHANVIEN");
            entity.Property(e => e.Phieunhapkhoidpnk).HasColumnName("PHIEUNHAPKHOIDPNK");
            entity.Property(e => e.Soptt)
                .HasMaxLength(255)
                .HasColumnName("SOPTT");
            entity.Property(e => e.Sotienthanhtoan).HasColumnName("SOTIENTHANHTOAN");

            entity.HasOne(d => d.HtttidhtttNavigation).WithMany(p => p.Phieuthanhtoans)
                .HasForeignKey(d => d.Htttidhttt)
                .HasConstraintName("FK_PTT_IDHTTT");

            entity.HasOne(d => d.NhanvienidnhanvienNavigation).WithMany(p => p.Phieuthanhtoans)
                .HasForeignKey(d => d.Nhanvienidnhanvien)
                .HasConstraintName("FK_PTT_IDNHANVIEN");

            entity.HasOne(d => d.PhieunhapkhoidpnkNavigation).WithMany(p => p.Phieuthanhtoans)
                .HasForeignKey(d => d.Phieunhapkhoidpnk)
                .HasConstraintName("FK_PTT_IDPNK");
        });

        modelBuilder.Entity<Phieuthutien>(entity =>
        {
            entity.HasKey(e => e.Idpthu).HasName("PK__PHIEUTHU__8F8F1EF10ACCB0CF");

            entity.ToTable("PHIEUTHUTIEN");

            entity.Property(e => e.Idpthu).HasColumnName("IDPTHU");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Htttidhttt).HasColumnName("HTTTIDHTTT");
            entity.Property(e => e.Ngaythutien)
                .HasColumnType("date")
                .HasColumnName("NGAYTHUTIEN");
            entity.Property(e => e.Nhanvienidnhanvien).HasColumnName("NHANVIENIDNHANVIEN");
            entity.Property(e => e.Phieuxuatkhoidpxk).HasColumnName("PHIEUXUATKHOIDPXK");
            entity.Property(e => e.Sopthu)
                .HasMaxLength(255)
                .HasColumnName("SOPTHU");
            entity.Property(e => e.Sotienthu).HasColumnName("SOTIENTHU");

            entity.HasOne(d => d.HtttidhtttNavigation).WithMany(p => p.Phieuthutiens)
                .HasForeignKey(d => d.Htttidhttt)
                .HasConstraintName("FK_PTHU_IDHTTT");

            entity.HasOne(d => d.NhanvienidnhanvienNavigation).WithMany(p => p.Phieuthutiens)
                .HasForeignKey(d => d.Nhanvienidnhanvien)
                .HasConstraintName("FK_PTHU_IDNHANVIEN");

            entity.HasOne(d => d.PhieuxuatkhoidpxkNavigation).WithMany(p => p.Phieuthutiens)
                .HasForeignKey(d => d.Phieuxuatkhoidpxk)
                .HasConstraintName("FK_PTHU_IDPXK");
        });

        modelBuilder.Entity<Phieuxuatkho>(entity =>
        {
            entity.HasKey(e => e.Idpxk).HasName("PK__PHIEUXUA__98FA1D5002A3FD3A");

            entity.ToTable("PHIEUXUATKHO");

            entity.Property(e => e.Idpxk).HasColumnName("IDPXK");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Donvivanchuyeniddvvc).HasColumnName("DONVIVANCHUYENIDDVVC");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Khachhangidkhachhang).HasColumnName("KHACHHANGIDKHACHHANG");
            entity.Property(e => e.Khoiluong).HasColumnName("KHOILUONG");
            entity.Property(e => e.Ngaydat)
                .HasColumnType("date")
                .HasColumnName("NGAYDAT");
            entity.Property(e => e.Ngaygiaohang)
                .HasColumnType("date")
                .HasColumnName("NGAYGIAOHANG");
            entity.Property(e => e.Ngaytiepnhan)
                .HasColumnType("date")
                .HasColumnName("NGAYTIEPNHAN");
            entity.Property(e => e.Nhanvienidnhanvien).HasColumnName("NHANVIENIDNHANVIEN");
            entity.Property(e => e.Sopxk)
                .HasMaxLength(255)
                .HasColumnName("SOPXK");
            entity.Property(e => e.Sotiendathu).HasColumnName("SOTIENDATHU");
            entity.Property(e => e.Tongtien).HasColumnName("TONGTIEN");
            entity.Property(e => e.Trangthaiidtt).HasColumnName("TRANGTHAIIDTT");

            entity.HasOne(d => d.DonvivanchuyeniddvvcNavigation).WithMany(p => p.Phieuxuatkhos)
                .HasForeignKey(d => d.Donvivanchuyeniddvvc)
                .HasConstraintName("FK_PXK_IDDVVC");

            entity.HasOne(d => d.KhachhangidkhachhangNavigation).WithMany(p => p.Phieuxuatkhos)
                .HasForeignKey(d => d.Khachhangidkhachhang)
                .HasConstraintName("FK_PXK_IDKHACHHANG");

            entity.HasOne(d => d.NhanvienidnhanvienNavigation).WithMany(p => p.Phieuxuatkhos)
                .HasForeignKey(d => d.Nhanvienidnhanvien)
                .HasConstraintName("FK_PXK_IDNHANVIEN");

            entity.HasOne(d => d.TrangthaiidttNavigation).WithMany(p => p.Phieuxuatkhos)
                .HasForeignKey(d => d.Trangthaiidtt)
                .HasConstraintName("FK_PXK_IDTT");
        });

        modelBuilder.Entity<Quycachdonggoi>(entity =>
        {
            entity.HasKey(e => e.Idquycach).HasName("PK__QUYCACHD__58536972868347AF");

            entity.ToTable("QUYCACHDONGGOI");

            entity.Property(e => e.Idquycach).HasColumnName("IDQUYCACH");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Dientichbemat).HasColumnName("DIENTICHBEMAT");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Khoiluong).HasColumnName("KHOILUONG");
            entity.Property(e => e.Maquycach)
                .HasMaxLength(255)
                .HasColumnName("MAQUYCACH");
            entity.Property(e => e.Sovien).HasColumnName("SOVIEN");
            entity.Property(e => e.Tenquycach)
                .HasMaxLength(255)
                .HasColumnName("TENQUYCACH");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.Idtaikhoan).HasName("PK__TAIKHOAN__FB332DA247BD3B14");

            entity.ToTable("TAIKHOAN");

            entity.Property(e => e.Idtaikhoan).HasColumnName("IDTAIKHOAN");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(255)
                .HasColumnName("MATKHAU");
            entity.Property(e => e.Tendangnhap)
                .HasMaxLength(255)
                .HasColumnName("TENDANGNHAP");
            entity.Property(e => e.Vaitro)
                .HasMaxLength(255)
                .HasColumnName("VAITRO");
        });

        modelBuilder.Entity<Thongtinxe>(entity =>
        {
            entity.HasKey(e => e.Idttxe);

            entity.ToTable("THONGTINXE");

            entity.Property(e => e.Idttxe)
                .ValueGeneratedNever()
                .HasColumnName("IDTTXE");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Phieuxuatkhoidpxk).HasColumnName("PHIEUXUATKHOIDPXK");
            entity.Property(e => e.Soluongxe).HasColumnName("SOLUONGXE");
            entity.Property(e => e.Xeidxe).HasColumnName("XEIDXE");

            entity.HasOne(d => d.PhieuxuatkhoidpxkNavigation).WithMany(p => p.Thongtinxes)
                .HasForeignKey(d => d.Phieuxuatkhoidpxk)
                .HasConstraintName("FK_THONGTINXE_PHIEUXUATKHO");

            entity.HasOne(d => d.XeidxeNavigation).WithMany(p => p.Thongtinxes)
                .HasForeignKey(d => d.Xeidxe)
                .HasConstraintName("FK_THONGTINXE_XE");
        });

        modelBuilder.Entity<Trangthai>(entity =>
        {
            entity.HasKey(e => e.Idtt).HasName("PK__TRANGTHA__B87C3AF8BD0D0A26");

            entity.ToTable("TRANGTHAI");

            entity.Property(e => e.Idtt).HasColumnName("IDTT");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Matt)
                .HasMaxLength(255)
                .HasColumnName("MATT");
            entity.Property(e => e.Tentt)
                .HasMaxLength(255)
                .HasColumnName("TENTT");
        });

        modelBuilder.Entity<Ungdung>(entity =>
        {
            entity.HasKey(e => e.Idungdung).HasName("PK__UNGDUNG__5FBD95B6270E8828");

            entity.ToTable("UNGDUNG");

            entity.Property(e => e.Idungdung).HasColumnName("IDUNGDUNG");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Maungdung)
                .HasMaxLength(255)
                .HasColumnName("MAUNGDUNG");
            entity.Property(e => e.Tenungdung)
                .HasMaxLength(255)
                .HasColumnName("TENUNGDUNG");
        });

        modelBuilder.Entity<Xe>(entity =>
        {
            entity.HasKey(e => e.Idxe).HasName("PK__XE__B87E9C20616A1B14");

            entity.ToTable("XE");

            entity.Property(e => e.Idxe).HasColumnName("IDXE");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Donvivanchuyeniddvvc).HasColumnName("DONVIVANCHUYENIDDVVC");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Maxe)
                .HasMaxLength(255)
                .HasColumnName("MAXE");
            entity.Property(e => e.Sokhoi).HasColumnName("SOKHOI");
            entity.Property(e => e.Tenxe)
                .HasMaxLength(255)
                .HasColumnName("TENXE");
            entity.Property(e => e.Trongtai).HasColumnName("TRONGTAI");

            entity.HasOne(d => d.DonvivanchuyeniddvvcNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.Donvivanchuyeniddvvc)
                .HasConstraintName("FK_XE_DONVIVANCHUYEN");
        });

        modelBuilder.Entity<Xuatxu>(entity =>
        {
            entity.HasKey(e => e.Idxuatxu).HasName("PK__XUATXU__F06E9ECE666BC05D");

            entity.ToTable("XUATXU");

            entity.Property(e => e.Idxuatxu).HasColumnName("IDXUATXU");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Maxuatxu)
                .HasMaxLength(255)
                .HasColumnName("MAXUATXU");
            entity.Property(e => e.Xuatxu1)
                .HasMaxLength(255)
                .HasColumnName("XUATXU");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
