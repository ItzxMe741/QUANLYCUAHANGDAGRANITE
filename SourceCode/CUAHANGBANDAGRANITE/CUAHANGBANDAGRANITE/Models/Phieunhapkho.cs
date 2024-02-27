using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Phieunhapkho
{
    public int Idpnk { get; set; }

    public int? Nhacungcapidncc { get; set; }

    public int? Nhanvienidnhanvien { get; set; }

    public int? Trangthaiidtt { get; set; }

    public int? Donvivanchuyeniddvvc { get; set; }

    public string? Sopnk { get; set; }

	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
	public DateTime? Ngaydat { get; set; }

	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
	public DateTime? Ngaytiepnhan { get; set; }

	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
	public DateTime? Ngaygiaohang { get; set; }

    public double? Tongtien { get; set; }

    public double? Sotiendathanhtoan { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual List<Chitietnhap> Chitietnhaps { get; set; } = new List<Chitietnhap>();

    public virtual Donvivanchuyen? DonvivanchuyeniddvvcNavigation { get; set; }

    public virtual Nhacungcap? NhacungcapidnccNavigation { get; set; }

    public virtual Nhanvien? NhanvienidnhanvienNavigation { get; set; }

    public virtual ICollection<Phieuthanhtoan> Phieuthanhtoans { get; set; } = new List<Phieuthanhtoan>();

    public virtual Trangthai? TrangthaiidttNavigation { get; set; }
}
