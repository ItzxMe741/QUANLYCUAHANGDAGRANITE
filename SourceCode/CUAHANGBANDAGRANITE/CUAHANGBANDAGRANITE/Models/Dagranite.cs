using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Dagranite
{
    public int Idda { get; set; }

    public int? Loaidaidloai { get; set; }

    public int? Xuatxuidxuatxu { get; set; }

    public string? Mada { get; set; }

    public string? Tenda { get; set; }

    public string? Tengoikhac { get; set; }

	[NotMapped]
	public required IFormFile FrontImage { get; set; }
	public string? Image { get; set; }

    public string? Dvt { get; set; }

    public double? Chieudai { get; set; }

    public double? Chieurong { get; set; }

    public double? Dientichbemat { get; set; }

    public double? Khoiluong { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual Loaidum? LoaidaidloaiNavigation { get; set; }

	public virtual ICollection<Loaithung> Loaithungs { get; set; } = new List<Loaithung>();

    public virtual IList<Noidungungdung> Noidungungdungs { get; set; } = new List<Noidungungdung>();

    public virtual Xuatxu? XuatxuidxuatxuNavigation { get; set; }
}
