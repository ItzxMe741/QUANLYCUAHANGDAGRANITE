using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Donvivanchuyen
{
    public int Iddvvc { get; set; }

    public int? Taikhoanidtaikhoan { get; set; }

    public string? Madvvc { get; set; }

    public string? Tendvvc { get; set; }

    public string? Diachi { get; set; }

    public string? Masothue { get; set; }

	[NotMapped]
	public required IFormFile FrontImage { get; set; }
	public string? Image { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Phieunhapkho> Phieunhapkhos { get; set; } = new List<Phieunhapkho>();

    public virtual ICollection<Phieuxuatkho> Phieuxuatkhos { get; set; } = new List<Phieuxuatkho>();

    public virtual Taikhoan? TaikhoanidtaikhoanNavigation { get; set; }

    public virtual ICollection<Xe> Xes { get; set; } = new List<Xe>();
}
