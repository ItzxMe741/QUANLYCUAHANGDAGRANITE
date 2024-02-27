using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Phieuthanhtoan
{
    public int Idptt { get; set; }

    public int? Htttidhttt { get; set; }

    public int? Nhanvienidnhanvien { get; set; }

    public int? Phieunhapkhoidpnk { get; set; }

    public string? Soptt { get; set; }

    public double? Sotienthanhtoan { get; set; }

	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
	public DateTime? Ngaythanhtoan { get; set; }

    public bool? Active { get; set; }

    public virtual Hinhthucthanhtoan? HtttidhtttNavigation { get; set; }

    public virtual Nhanvien? NhanvienidnhanvienNavigation { get; set; }

    public virtual Phieunhapkho? PhieunhapkhoidpnkNavigation { get; set; }
}
