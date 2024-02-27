using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Phieuxuatkho
{
    public int Idpxk { get; set; }

    public int? Khachhangidkhachhang { get; set; }

    public int? Nhanvienidnhanvien { get; set; }

    public int? Trangthaiidtt { get; set; }

    public int? Donvivanchuyeniddvvc { get; set; }

    public string? Sopxk { get; set; }

	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
	public DateTime? Ngaydat { get; set; }

	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
	public DateTime? Ngaytiepnhan { get; set; }

	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
	public DateTime? Ngaygiaohang { get; set; }

    public double? Tongtien { get; set; }

    public double? Sotiendathu { get; set; }

    public double? Khoiluong { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual List<Chitietxuat> Chitietxuats { get; set; } = new List<Chitietxuat>();

    public virtual Donvivanchuyen? DonvivanchuyeniddvvcNavigation { get; set; }

    public virtual Khachhang? KhachhangidkhachhangNavigation { get; set; }

    public virtual Nhanvien? NhanvienidnhanvienNavigation { get; set; }

    public virtual ICollection<Phieuthutien> Phieuthutiens { get; set; } = new List<Phieuthutien>();

    public virtual ICollection<Thongtinxe> Thongtinxes { get; set; } = new List<Thongtinxe>();

    public virtual Trangthai? TrangthaiidttNavigation { get; set; }
}
