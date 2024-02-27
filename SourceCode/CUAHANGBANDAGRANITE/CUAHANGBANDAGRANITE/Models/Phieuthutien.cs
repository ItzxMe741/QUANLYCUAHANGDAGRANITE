using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Phieuthutien
{
    public int Idpthu { get; set; }

    public int? Htttidhttt { get; set; }

    public int? Nhanvienidnhanvien { get; set; }

    public int? Phieuxuatkhoidpxk { get; set; }

    public string? Sopthu { get; set; }

    public double? Sotienthu { get; set; }

	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
	public DateTime? Ngaythutien { get; set; }

    public bool? Active { get; set; }

    public virtual Hinhthucthanhtoan? HtttidhtttNavigation { get; set; }

    public virtual Nhanvien? NhanvienidnhanvienNavigation { get; set; }

    public virtual Phieuxuatkho? PhieuxuatkhoidpxkNavigation { get; set; }
}
